using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("Health assigned in Awake. The current scene uses 1, so one valid attack kills the enemy.")]
    [Min(1)]
    public int maxHealth = 1;

    [Tooltip("Runtime health value. Awake resets it from Max Health; do not use this as the starting-health setting.")]
    public int currentHealth;

    [Header("Death Behaviour")]
    [Tooltip("If enabled, remove the enemy GameObject on death. If disabled, keep it in the scene but make it inactive.")]
    public bool destroyOnDeath = true;

    private bool deathHandled;

    public bool IsDead => deathHandled || currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void KillInstantly()
    {
        if (IsDead)
        {
            return;
        }

        currentHealth = 0;
        Die();
    }

    private void Die()
    {
        if (deathHandled)
        {
            return;
        }

        deathHandled = true;

        DeathEffectSpawner deathEffectSpawner = GetComponent<DeathEffectSpawner>();

        if (deathEffectSpawner != null)
        {
            deathEffectSpawner.SpawnDeathEffectOnce();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterEnemyKilled();
        }

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
