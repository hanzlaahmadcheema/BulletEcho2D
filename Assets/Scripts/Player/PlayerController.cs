using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Player movement speed in world units per second. Input comes from WASD or arrow keys.")]
    [Min(0f)]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.gameEnded)
        {
            movement = Vector2.zero;
            return;
        }

        if (Keyboard.current == null)
        {
            movement = Vector2.zero;
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal -= 1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal += 1f;
        }

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            vertical -= 1f;
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            vertical += 1f;
        }

        movement = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.gameEnded)
        {
            return;
        }

        Vector2 nextPosition =
            rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }
}
