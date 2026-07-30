using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene Connection")]
    [Tooltip("UIManager on the Canvas. If empty, GameManager searches for one during Awake or Start.")]
    public UIManager uiManager;

    [Header("Runtime Progress (calculated automatically)")]
    [Tooltip("Number of EnemyHealth components found when the scene starts.")]
    public int totalEnemies;

    [Tooltip("Number of enemies that have reported death.")]
    public int killedEnemies;

    [Tooltip("Number of CashCollectible components found when the scene starts.")]
    public int totalCash;

    [Tooltip("Objective value collected so far.")]
    public int collectedCash;

    [Tooltip("True after either the win or lose condition. Gameplay scripts use this to stop acting.")]
    public bool gameEnded;

    private bool warnedMissingUIManager;

    private void Awake()
    {
        Instance = this;
        ResolveUIManager();
    }

    private void Start()
    {
        totalEnemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None).Length;
        totalCash = FindObjectsByType<CashCollectible>(FindObjectsSortMode.None).Length;

        if (ResolveUIManager())
        {
            uiManager.UpdateEnemies(killedEnemies, totalEnemies);
            uiManager.UpdateCash(collectedCash, totalCash);
        }
    }

    public void RegisterEnemyKilled()
    {
        if (gameEnded)
        {
            return;
        }

        killedEnemies++;

        if (ResolveUIManager())
        {
            uiManager.UpdateEnemies(killedEnemies, totalEnemies);
        }

        CheckWinCondition();
    }

    public void CollectCash(int amount)
    {
        if (gameEnded)
        {
            return;
        }

        collectedCash += amount;

        if (ResolveUIManager())
        {
            uiManager.UpdateCash(collectedCash, totalCash);
        }

        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (!gameEnded && killedEnemies >= totalEnemies && collectedCash >= totalCash)
        {
            gameEnded = true;
            DisablePlayerGameplay();

            if (ResolveUIManager())
            {
                uiManager.ShowWin();
            }
        }
    }

    public void LoseGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;
        DisablePlayerGameplay();

        if (ResolveUIManager())
        {
            uiManager.ShowLose();
        }
    }

    private bool ResolveUIManager()
    {
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }

        if (uiManager != null)
        {
            return true;
        }

        if (!warnedMissingUIManager)
        {
            warnedMissingUIManager = true;
        }

        return false;
    }

    private void DisablePlayerGameplay()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        PlayerAttack playerAttack = FindFirstObjectByType<PlayerAttack>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (playerAttack != null)
        {
            playerAttack.enabled = false;
        }
    }
}
