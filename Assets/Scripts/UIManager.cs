using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Referencias")]
    public GameObject winPanel;
    public Button restartButton;
    public Button nextLevelButton;
    public Button menuButton;

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

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Debug.Log("Siguiente nivel...");

        // Por ahora, reinicia el mismo nivel
        // Más adelante puedes cargar otro nivel
        RestartLevel();
    }

    public void BackToMenu()
    {
        Debug.Log("Volviendo al menú...");
        SceneManager.LoadScene("MenuScene");
    }
}