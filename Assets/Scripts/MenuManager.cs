using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private OptionsManager optionsManager;

    [Header("Panel de Confirmación")]
    public GameObject confirmResetPanel;

    void Start()
    {
        // Obtenemos el componente de opciones que está en el mismo objeto
        optionsManager = GetComponent<OptionsManager>();

        // Nos aseguramos de que el panel de confirmación esté oculto al arrancar
        if (confirmResetPanel != null)
        {
            confirmResetPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        // Lógica para empezar a jugar: intenta cargar el nivel actual guardado
        if (LevelManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance.GetCurrentLevel();
            Debug.Log("Cargando nivel: " + currentLevel);
            LevelManager.Instance.LoadLevel(currentLevel);
        }
        else
        {
            // Si no hay LevelManager, carga la escena de juego básica por defecto
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OpenOptions()
    {
        // Llama a la función del OptionsManager para abrir el panel de ajustes
        if (optionsManager != null)
        {
            optionsManager.OpenOptions();
        }
    }

    // Activa el panel de aviso cuando el usuario quiere borrar sus datos
    public void ShowResetConfirmation()
    {
        if (confirmResetPanel != null)
        {
            confirmResetPanel.SetActive(true);
        }
    }

    // Simplemente cierra el panel si el usuario se arrepiente del reset
    public void CancelReset()
    {
        if (confirmResetPanel != null)
        {
            confirmResetPanel.SetActive(false);
        }
    }

    // Función definitiva para borrar todo el progreso del jugador
    public void ConfirmReset()
    {
        Debug.Log("=== RESETEAR PROGRESO ===");

        // Avisa al LevelManager para que vuelva al nivel inicial
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ResetProgress();
        }

        // Borra físicamente todos los datos guardados en PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Progreso reseteado. Volviendo al Nivel 1...");

        // Recargamos el menú para que se actualicen los textos y botones
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");

        // Cierra la aplicación (solo funciona en el juego ya buildeado)
        Application.Quit();

        // Este código especial hace que también se detenga el PlayMode en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}