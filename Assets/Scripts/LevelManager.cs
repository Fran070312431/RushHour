using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Configuración de Niveles")]
    public int currentLevel = 0; // Índice del nivel actual (0 es la primera escena de juego)
    public int totalLevels = 5; // Número total de niveles disponibles en el proyecto

    void Awake()
    {
        // Aplicamos el patrón Singleton para centralizar el control de escenas
        if (Instance == null)
        {
            Instance = this;
            // Evitamos que este objeto se destruya para mantener el rastro del nivel al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si por error se crea otro LevelManager, lo eliminamos para no duplicar lógica
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Al arrancar el juego, recuperamos el último nivel que el jugador alcanzó
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        // Guardamos el progreso en el dispositivo para que persista al cerrar el juego
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.Save();

        // Obtenemos el nombre del archivo de la escena y lo cargamos
        string sceneName = GetSceneName(levelNumber);
        Debug.Log("Cargando escena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    string GetSceneName(int levelNumber)
    {
        // Lógica para diferenciar el nombre de la primera escena de las siguientes (1, 2, 3...)
        if (levelNumber == 0)
        {
            return "GameScene"; // Nombre exacto de la primera escena de juego
        }
        else
        {
            return "GameScene" + levelNumber; // Construye nombres tipo "GameScene1", "GameScene2", etc.
        }
    }

    public void NextLevel()
    {
        // Avanzamos el contador de nivel
        currentLevel++;

        // Comprobamos si el jugador ha superado el último nivel disponible
        if (currentLevel >= totalLevels)
        {
            Debug.Log("¡Completaste todos los niveles!");
            ShowCompletionMessage();
            BackToMenu(); // Si termina todo, lo devolvemos al menú principal
        }
        else
        {
            // Si quedan niveles, cargamos el siguiente
            LoadLevel(currentLevel);
        }
    }

    public void RestartCurrentLevel()
    {
        // Recarga la escena actual en caso de que el jugador quiera reintentar el puzzle
        LoadLevel(currentLevel);
    }

    public void BackToMenu()
    {
        // Carga la escena del menú principal
        SceneManager.LoadScene("MenuScene");
    }

    public int GetCurrentLevel()
    {
        // Devuelve el índice interno del nivel actual
        return currentLevel;
    }

    public int GetLevelDisplayNumber()
    {
        // Sumamos 1 al índice para que el usuario vea "Nivel 1" en lugar de "Nivel 0"
        return currentLevel + 1;
    }

    public void ResetProgress()
    {
        // Reiniciamos los valores de progreso tanto en el script como en los datos guardados
        currentLevel = 0;
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.Save();
    }

    void ShowCompletionMessage()
    {
        // Mensaje de depuración para confirmar el fin del flujo de juego en la consola
        Debug.Log("=== ¡FELICIDADES! COMPLETASTE TODOS LOS NIVELES ===");
    }
}