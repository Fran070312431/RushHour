using UnityEngine;

public class SimpleGameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Reiniciar con R
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
