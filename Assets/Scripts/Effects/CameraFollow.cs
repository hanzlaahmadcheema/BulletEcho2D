using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Scene Connection")]
    [Tooltip("Transform of the Player GameObject. The camera reads this object's world position every frame.")]
    public Transform target;

    [Header("Follow Settings")]
    [Tooltip("How quickly the camera catches the target. Larger values feel more immediate; smaller values feel softer.")]
    public float smoothSpeed = 8f;

    [Tooltip("Smallest X and Y coordinates the camera is allowed to reach.")]
    public Vector2 minBounds = new Vector2(-7f, -6f);

    [Tooltip("Largest X and Y coordinates the camera is allowed to reach.")]
    public Vector2 maxBounds = new Vector2(7f, 14f);

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y),
            -10f
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
