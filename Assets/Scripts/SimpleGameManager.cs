using UnityEngine;

public class SimpleGameManager : MonoBehaviour
{
    // El Update se ejecuta en cada frame del juego (unas 60 veces por segundo)
    void Update()
    {
        // Añadimos un atajo de teclado: si pulsas la tecla 'R' se reinicia el juego
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Cargamos la primera escena del proyecto (índice 0)
            // Esto es muy útil durante el desarrollo para resetear rápido sin ir al menú
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}