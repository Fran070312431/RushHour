using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Referencias")]
    public GameObject winPanel;
    public UnityEngine.UI.Button restartButton;  // CORREGIDO
    public UnityEngine.UI.Button nextLevelButton;  // CORREGIDO
    public UnityEngine.UI.Button menuButton;  // CORREGIDO
    public TextMeshProUGUI levelText;

    private bool isChangingLevel = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        UpdateLevelText();

        // LIMPIAR listeners antes de añadir
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(NextLevel);
        }

        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(BackToMenu);
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null && LevelManager.Instance != null)
        {
            int displayLevel = LevelManager.Instance.GetLevelDisplayNumber();
            levelText.text = "NIVEL " + displayLevel;
        }
    }

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Reiniciando nivel...");

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RestartCurrentLevel();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void NextLevel()
    {
        // Prevenir llamadas duplicadas
        if (isChangingLevel)
        {
            Debug.LogWarning("NextLevel ya está en progreso, ignorando llamada duplicada");
            return;
        }

        isChangingLevel = true;
        Debug.Log("=== NextLevel() llamado ===");

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NextLevel();
        }
        else
        {
            Debug.LogWarning("LevelManager no encontrado");
            RestartLevel();
        }
    }

    public void BackToMenu()
    {
        Debug.Log("Volviendo al menú...");

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.BackToMenu();
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}