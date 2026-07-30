using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("Starting and maximum player health. Awake copies this value into Current Health.")]
    [Min(1)]
    public int maxHealth = 5;

    [Tooltip("Runtime health value shown by the HUD. It is reset from Max Health when the scene starts.")]
    public int currentHealth;

    private bool deathHandled;

    public bool IsDead => deathHandled || currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || GameManager.Instance == null || GameManager.Instance.gameEnded)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        UpdateHealthUI();

        if (IsDead)
        {
            Die();
        }
    }

    public void KillInstantly()
    {
        if (IsDead || GameManager.Instance == null || GameManager.Instance.gameEnded)
        {
            return;
        }

        currentHealth = 0;
        UpdateHealthUI();
        Die();
    }

    public void Heal(int amount)
    {
        if (IsDead)
        {
            return;
        }

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (GameManager.Instance != null && GameManager.Instance.uiManager != null)
        {
            GameManager.Instance.uiManager.UpdateHealth(currentHealth, maxHealth);
        }
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
            GameManager.Instance.LoseGame();
        }
    }
}
