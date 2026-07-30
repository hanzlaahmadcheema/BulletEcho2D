using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HUD Text Connections")]
    [Tooltip("Text component that displays current and maximum player health.")]
    public Text healthText;

    [Tooltip("Text component that displays collected and total objectives.")]
    public Text cashText;

    [Tooltip("Text component that displays defeated and total enemies.")]
    public Text enemyText;

    [Header("Result Panel Connections")]
    [Tooltip("Canvas child enabled when every enemy and objective is complete.")]
    public GameObject winPanel;

    [Tooltip("Canvas child enabled when PlayerHealth reaches zero.")]
    public GameObject losePanel;

    private bool warnedHealthText;
    private bool warnedCashText;
    private bool warnedEnemyText;
    private bool warnedWinPanel;
    private bool warnedLosePanel;

    private void Awake()
    {
        ResolveReferences();
        SimplifyCanvas();
        EnsureEventSystem();
        CreateGameButtons();

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        else
        {
            WarnOnce(ref warnedWinPanel, "UIManager: WinPanel reference is missing.");
        }

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
        else
        {
            WarnOnce(ref warnedLosePanel, "UIManager: LosePanel reference is missing.");
        }
    }

    public void UpdateHealth(int current, int max)
    {
        ResolveReferences();

        if (healthText != null)
        {
            healthText.text = $"HP  {current} / {max}";
        }
        else
        {
            WarnOnce(ref warnedHealthText, "UIManager: HealthText reference is missing.");
        }
    }

    public void UpdateCash(int collected, int total)
    {
        ResolveReferences();

        if (cashText != null)
        {
            cashText.text = $"CASH  {collected} / {total}";
        }
        else
        {
            WarnOnce(ref warnedCashText, "UIManager: CashText reference is missing.");
        }
    }

    public void UpdateEnemies(int killed, int total)
    {
        ResolveReferences();

        if (enemyText != null)
        {
            enemyText.text = $"TARGETS  {killed} / {total}";
        }
        else
        {
            WarnOnce(ref warnedEnemyText, "UIManager: EnemyText reference is missing.");
        }
    }

    public void ShowWin()
    {
        ResolveReferences();

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        else
        {
            WarnOnce(ref warnedWinPanel, "UIManager: WinPanel reference is missing.");
        }
    }

    public void ShowLose()
    {
        ResolveReferences();

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        else
        {
            WarnOnce(ref warnedLosePanel, "UIManager: LosePanel reference is missing.");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ResolveReferences()
    {
        if (healthText == null)
        {
            healthText = FindChildText("HealthText");
        }

        if (cashText == null)
        {
            cashText = FindChildText("CashText");
        }

        if (enemyText == null)
        {
            enemyText = FindChildText("EnemyText");
        }

        if (winPanel == null)
        {
            winPanel = FindChildObject("WinPanel");
        }

        if (losePanel == null)
        {
            losePanel = FindChildObject("LosePanel");
        }
    }

    private Text FindChildText(string childName)
    {
        Transform child = transform.Find(childName);
        return child != null ? child.GetComponent<Text>() : null;
    }

    private GameObject FindChildObject(string childName)
    {
        Transform child = transform.Find(childName);
        return child != null ? child.gameObject : null;
    }

    private void SimplifyCanvas()
    {
        string[] decorationNames =
        {
            "StatusPanel",
            "CashPanel",
            "LevelBadge",
            "HealthIcon",
            "ObjectiveIcon",
            "EnemyIcon"
        };

        foreach (string decorationName in decorationNames)
        {
            GameObject decoration = FindChildObject(decorationName);
            if (decoration != null)
            {
                decoration.SetActive(false);
            }
        }

        RemovePanelImage(winPanel);
        RemovePanelImage(losePanel);
    }

    private void RemovePanelImage(GameObject panel)
    {
        if (panel == null)
        {
            return;
        }

        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.enabled = false;
        }
    }

    private void EnsureEventSystem()
    {
        if (EventSystem.current != null)
        {
            return;
        }

        GameObject eventSystemObject = new GameObject("EventSystem");
        eventSystemObject.AddComponent<EventSystem>();
        InputSystemUIInputModule inputModule =
            eventSystemObject.AddComponent<InputSystemUIInputModule>();
        inputModule.AssignDefaultActions();
    }

    private void CreateGameButtons()
    {
        if (transform.Find("GameButtons") != null)
        {
            return;
        }

        GameObject buttonGroup = new GameObject("GameButtons", typeof(RectTransform));
        buttonGroup.transform.SetParent(transform, false);
        buttonGroup.transform.SetAsLastSibling();

        RectTransform groupRect = buttonGroup.GetComponent<RectTransform>();
        groupRect.anchorMin = new Vector2(0.5f, 0f);
        groupRect.anchorMax = new Vector2(0.5f, 0f);
        groupRect.pivot = new Vector2(0.5f, 0f);
        groupRect.anchoredPosition = new Vector2(0f, 24f);
        groupRect.sizeDelta = new Vector2(360f, 52f);

        CreateButton(buttonGroup.transform, "RestartButton", "RESTART", -92f, RestartGame);
        CreateButton(buttonGroup.transform, "EndGameButton", "END GAME", 92f, EndGame);
    }

    private void CreateButton(
        Transform parent,
        string objectName,
        string label,
        float xPosition,
        UnityEngine.Events.UnityAction action
    )
    {
        GameObject buttonObject = new GameObject(
            objectName,
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Image),
            typeof(Button)
        );
        buttonObject.transform.SetParent(parent, false);

        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.pivot = new Vector2(0.5f, 0.5f);
        buttonRect.anchoredPosition = new Vector2(xPosition, 0f);
        buttonRect.sizeDelta = new Vector2(168f, 48f);

        Image background = buttonObject.GetComponent<Image>();
        background.color = new Color(0.08f, 0.1f, 0.13f, 0.92f);

        Button button = buttonObject.GetComponent<Button>();
        button.targetGraphic = background;
        button.onClick.AddListener(action);

        GameObject textObject = new GameObject(
            "Text",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Text)
        );
        textObject.transform.SetParent(buttonObject.transform, false);

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        Text buttonText = textObject.GetComponent<Text>();
        buttonText.text = label;
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        buttonText.fontSize = 20;
        buttonText.fontStyle = FontStyle.Bold;
        buttonText.color = Color.white;
        buttonText.raycastTarget = false;
    }

    private void WarnOnce(ref bool alreadyWarned, string message)
    {
        if (alreadyWarned)
        {
            return;
        }

        alreadyWarned = true;
    }
}
