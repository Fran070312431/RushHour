using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [Header("Configuración de Niveles")]
    public int currentLevel = 0; // Ahora empieza en 0 (GameScene)
    public int totalLevels = 5; // Total: GameScene + GameScene1-4
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }
    
    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.Save();
        
        // Cargar la escena según el número
        string sceneName = GetSceneName(levelNumber);
        Debug.Log("Cargando escena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    
    string GetSceneName(int levelNumber)
    {
        if (levelNumber == 0)
        {
            return "GameScene"; // Nivel 0 = GameScene
        }
        else
        {
            return "GameScene" + levelNumber; // Nivel 1 = GameScene1, etc.
        }
    }
    
    public void NextLevel()
    {
        currentLevel++;
        
        if (currentLevel >= totalLevels)
        {
            Debug.Log("¡Completaste todos los niveles!");
            ShowCompletionMessage();
            BackToMenu();
        }
        else
        {
            LoadLevel(currentLevel);
        }
    }
    
    public void RestartCurrentLevel()
    {
        LoadLevel(currentLevel);
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    
    public int GetLevelDisplayNumber()
    {
        // Para mostrar al usuario: Nivel 1, 2, 3...
        return currentLevel + 1;
    }
    
    public void ResetProgress()
    {
        currentLevel = 0;
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.Save();
    }
    
    void ShowCompletionMessage()
    {
        // Aquí puedes mostrar un panel especial de "¡Juego Completado!"
        Debug.Log("=== ¡FELICIDADES! COMPLETASTE TODOS LOS NIVELES ===");
    }
}