using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Hacemos un Singleton para poder llamar al UI desde cualquier otro script fácilmente
    public static UIManager Instance;

    [Header("UI Referencias")]
    public GameObject winPanel;             // El panel que sale cuando ganas
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button nextLevelButton;
    public UnityEngine.UI.Button menuButton;
    public TextMeshProUGUI levelText;       // El texto que dice en qué nivel estamos

    // Variable para evitar que se pulse dos veces el botón de cambiar de nivel rápido
    private bool isChangingLevel = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Al empezar, nos aseguramos de que el panel de victoria esté escondido
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // Actualizamos el número del nivel en pantalla
        UpdateLevelText();

        // Configuramos los botones por código para que no den fallos de referencia
        // Primero limpiamos los eventos y luego añadimos la función correspondiente
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

    // Función sencilla para poner el número del nivel actual en el texto de la UI
    void UpdateLevelText()
    {
        if (levelText != null && LevelManager.Instance != null)
        {
            int displayLevel = LevelManager.Instance.GetLevelDisplayNumber();
            levelText.text = "NIVEL " + displayLevel;
        }
    }

    // Activamos el cartel de "Has ganado"
    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    // Lógica para el botón de reiniciar nivel
    public void RestartLevel()
    {
        Debug.Log("Reiniciando nivel...");

        // Si existe el LevelManager le pedimos que reinicie, si no, recargamos la escena a mano
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RestartCurrentLevel();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Lógica para pasar al siguiente nivel
    public void NextLevel()
    {
        // Control de seguridad: si ya le hemos dado, no hacemos nada más
        if (isChangingLevel)
        {
            Debug.LogWarning("NextLevel ya está en progreso, ignorando llamada duplicada");
            return;
        }

        isChangingLevel = true;
        Debug.Log("=== NextLevel() llamado ===");

        // Le pedimos al LevelManager que cargue la siguiente escena
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

    // Para salir del juego y volver a la pantalla principal
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