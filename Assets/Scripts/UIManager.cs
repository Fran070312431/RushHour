using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Referencias")]
    public GameObject winPanel;
    public Button restartButton;
    public Button nextLevelButton;
    public Button menuButton;
    public TextMeshProUGUI levelText; // NUEVO: Para mostrar "NIVEL 1", "NIVEL 2", etc.

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

        // NUEVO: Mostrar número de nivel
        UpdateLevelText();

        // Conectar botones
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartLevel);
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(NextLevel);
        }

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(BackToMenu);
        }
    }

    // NUEVO: Actualizar texto del nivel
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

        // ACTUALIZADO: Usar LevelManager si existe
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RestartCurrentLevel();
        }
        else
        {
            // Fallback: Recargar escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void NextLevel()
    {
        Debug.Log("Siguiente nivel...");

        // ACTUALIZADO: Usar LevelManager para cargar siguiente nivel
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NextLevel();
        }
        else
        {
            Debug.LogWarning("LevelManager no encontrado. No se puede cargar siguiente nivel.");
            // Fallback: Reiniciar nivel actual
            RestartLevel();
        }
    }

    public void BackToMenu()
    {
        Debug.Log("Volviendo al menú...");

        // ACTUALIZADO: Usar LevelManager si existe
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.BackToMenu();
        }
        else
        {
            // Fallback: Cargar menú directamente
            SceneManager.LoadScene("MenuScene");
        }
    }
}