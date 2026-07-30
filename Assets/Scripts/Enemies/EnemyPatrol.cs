using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Scene Connections")]
    [Tooltip("Ordered Transform references for this enemy's route. The enemy moves from element 0 to the last element, then loops.")]
    public Transform[] waypoints;

    [Header("Patrol Settings")]
    [Tooltip("World units moved per second through Rigidbody2D.MovePosition.")]
    [Min(0f)]
    public float moveSpeed = 2f;

    [Tooltip("Distance from a waypoint at which it counts as reached.")]
    [Min(0.001f)]
    public float reachDistance = 0.1f;

    [Tooltip("Seconds without meaningful movement before the script skips to the next waypoint.")]
    [Min(0f)]
    public float stuckTimeout = 1f;

    private int currentWaypointIndex;
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private Vector2 lastPosition;
    private float stuckTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        lastPosition = rb != null ? rb.position : (Vector2)transform.position;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.gameEnded)
        {
            return;
        }

        if (enemyHealth != null && enemyHealth.IsDead)
        {
            return;
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        Transform currentTarget = waypoints[currentWaypointIndex];

        if (currentTarget == null)
        {
            AdvanceWaypoint();
            return;
        }

        Vector2 currentPosition = rb != null ? rb.position : (Vector2)transform.position;
        Vector2 targetPosition = currentTarget.position;

        if (Vector2.Distance(currentPosition, targetPosition) <= reachDistance)
        {
            AdvanceWaypoint();
            return;
        }

        UpdateStuckState(currentPosition);

        if (stuckTime >= stuckTimeout)
        {
            AdvanceWaypoint();
            lastPosition = currentPosition;
            return;
        }

        Vector2 nextPosition = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            moveSpeed * Time.fixedDeltaTime
        );

        if (rb != null)
        {
            rb.WakeUp();
            rb.MovePosition(nextPosition);
        }
        else
        {
            transform.position = nextPosition;
        }

        if (Vector2.Distance(nextPosition, targetPosition) <= reachDistance)
        {
            AdvanceWaypoint();
        }
    }

    private void AdvanceWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        stuckTime = 0f;
    }

    private void UpdateStuckState(Vector2 currentPosition)
    {
        if (Vector2.Distance(currentPosition, lastPosition) <= 0.001f)
        {
            stuckTime += Time.fixedDeltaTime;
        }
        else
        {
            stuckTime = 0f;
        }

        lastPosition = currentPosition;
    }
}
