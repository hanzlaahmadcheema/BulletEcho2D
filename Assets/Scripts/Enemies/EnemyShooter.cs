using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Scene Connection")]
    [Tooltip("Transform of the Player GameObject. If empty at runtime, the script searches for PlayerHealth.")]
    public Transform player;

    [Header("Shooting Settings")]
    [Tooltip("Maximum world-space distance at which this enemy can damage the player.")]
    [Min(0f)]
    public float shootingRange = 2.8f;

    [Tooltip("Health points removed by one successful line-of-sight attack.")]
    [Min(0)]
    public int damage = 1;

    [Tooltip("Minimum seconds between successful attacks.")]
    [Min(0f)]
    public float shootCooldown = 1.5f;

    [Header("Physics Layer Connections")]
    [Tooltip("Wall layer included in the Linecast. A wall hit before the player blocks the attack.")]
    public LayerMask wallLayer;

    [Tooltip("Player layer included in the Linecast so the target can be detected.")]
    public LayerMask playerLayer;

    private float nextShootTime;
    private float nextVisionCheckTime;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.gameEnded)
        {
            return;
        }

        if (enemyHealth != null && enemyHealth.IsDead)
        {
            return;
        }

        if (player == null)
        {
            PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
            player = playerHealth != null ? playerHealth.transform : null;
        }

        if (player == null || Vector2.Distance(transform.position, player.position) > shootingRange)
        {
            return;
        }

        if (Time.time < nextShootTime || Time.time < nextVisionCheckTime)
        {
            return;
        }

        nextVisionCheckTime = Time.time + 0.1f;

        if (!HasClearLineOfSight())
        {
            return;
        }

        PlayerHealth targetHealth = player.GetComponent<PlayerHealth>();

        if (targetHealth == null)
        {
            targetHealth = player.GetComponentInParent<PlayerHealth>();
        }

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            nextShootTime = Time.time + shootCooldown;
        }
    }

    private bool HasClearLineOfSight()
    {
        int combinedMask = wallLayer.value | playerLayer.value;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, combinedMask);

        if (hit.collider == null)
        {
            return false;
        }

        PlayerHealth hitPlayer = hit.collider.GetComponent<PlayerHealth>();

        if (hitPlayer == null)
        {
            hitPlayer = hit.collider.GetComponentInParent<PlayerHealth>();
        }

        return hitPlayer != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
