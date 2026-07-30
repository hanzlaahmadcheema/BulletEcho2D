using UnityEngine;

public class PlayerContactKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryKillEnemy(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryKillEnemy(other);
    }

    private void TryKillEnemy(Collider2D other)
    {
        if (other == null || (GameManager.Instance != null && GameManager.Instance.gameEnded))
        {
            return;
        }

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
        {
            enemyHealth = other.GetComponentInParent<EnemyHealth>();
        }

        if (enemyHealth != null && !enemyHealth.IsDead)
        {
            enemyHealth.KillInstantly();
        }
    }
}
