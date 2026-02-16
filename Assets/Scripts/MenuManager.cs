using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class MenuManager : MonoBehaviour
{
    private OptionsManager optionsManager;

    void Start()
    {
        optionsManager = GetComponent<OptionsManager>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Cambia por el nombre de tu escena
    }

    public void OpenOptions()
    {
        if (optionsManager != null)
        {
            optionsManager.OpenOptions();
        }
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