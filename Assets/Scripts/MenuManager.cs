
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private OptionsManager optionsManager;

    [Header("Panel de Confirmación")]
    public GameObject confirmResetPanel;

    void Start()
    {
        optionsManager = GetComponent<OptionsManager>();

        // Ocultar panel de confirmación al inicio
        if (confirmResetPanel != null)
        {
            confirmResetPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        if (LevelManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance.GetCurrentLevel();
            Debug.Log("Cargando nivel: " + currentLevel);
            LevelManager.Instance.LoadLevel(currentLevel);
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OpenOptions()
    {
        if (optionsManager != null)
        {
            optionsManager.OpenOptions();
        }
    }

    // Mostrar panel de confirmación
    public void ShowResetConfirmation()
    {
        if (confirmResetPanel != null)
        {
            confirmResetPanel.SetActive(true);
        }
    }

    // Cancelar reset
    public void CancelReset()
    {
        if (confirmResetPanel != null)
        {
            confirmResetPanel.SetActive(false);
        }
    }

    // Confirmar reset
    public void ConfirmReset()
    {
        Debug.Log("=== RESETEAR PROGRESO ===");

        // Resetear LevelManager
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ResetProgress();
        }

        // Borrar todos los datos guardados
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Progreso reseteado. Volviendo al Nivel 1...");

        // Recargar el menú
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}