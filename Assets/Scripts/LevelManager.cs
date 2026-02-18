using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [Header("Configuración de Niveles")]
    public int currentLevel = 1;
    public int totalLevels = 5; // Cambia esto según cuántos niveles tengas
    
    void Awake()
    {
        // Singleton que persiste entre escenas
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
        // Cargar el nivel guardado
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    
    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.Save();
        
        // Cargar la escena del nivel
        SceneManager.LoadScene("Level" + levelNumber);
    }
    
    public void NextLevel()
    {
        currentLevel++;
        
        if (currentLevel > totalLevels)
        {
            // Si completó todos los niveles, volver al menú
            Debug.Log("¡Completaste todos los niveles!");
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
    
    public void ResetProgress()
    {
        currentLevel = 1;
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.Save();
    }
}
```

---

## **PASO 2: Crear las Escenas de los Niveles**

### **Opción A: Duplicar la Escena Actual (Más Fácil)**

1. **En la carpeta Scenes, click derecho en GameScene**
2. **Duplicate** (Ctrl+D)
3. Renombrar a **"Level1"**
4. Duplicar de nuevo y renombrar a **"Level2"**
5. Repetir para **"Level3"**, **"Level4"**, etc.

### **Opción B: Crear Escenas Nuevas**

1. **File ? New Scene**
2. Copia todo de GameScene (Canvas, GridManager, etc.)
3. **File ? Save As ? "Level1"**
4. Repetir para Level2, Level3, etc.

---

## **PASO 3: Configurar Build Settings**

1. **File ? Build Settings**
2. **Añadir todas las escenas:**
```
   [0] MenuScene
   [1] Level1
   [2] Level2
   [3] Level3
   [4] Level4
   [5] Level5
```
3. **Cerrar Build Settings**

---

## **PASO 4: Añadir LevelManager al Menú**

1. **Abre MenuScene**
2. **GameObject ? Create Empty**
3. Renombrar a **"LevelManager"**
4. **Add Component ? LevelManager**
5. Configurar:
```
   Current Level: 1
   Total Levels: 5 (o los que hayas creado)using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [Header("Configuración de Niveles")]
    public int currentLevel = 1;
    public int totalLevels = 5; // Cambia esto según cuántos niveles tengas
    
    void Awake()
    {
        // Singleton que persiste entre escenas
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
        // Cargar el nivel guardado
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    
    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.Save();
        
        // Cargar la escena del nivel
        SceneManager.LoadScene("Level" + levelNumber);
    }
    
    public void NextLevel()
    {
        currentLevel++;
        
        if (currentLevel > totalLevels)
        {
            // Si completó todos los niveles, volver al menú
            Debug.Log("¡Completaste todos los niveles!");
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
    
    public void ResetProgress()
    {
        currentLevel = 1;
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.Save();
    }
}