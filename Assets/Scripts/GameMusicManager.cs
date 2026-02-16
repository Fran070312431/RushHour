using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    public AudioClip gameMusic; // Música diferente para el juego

    void Start()
    {
        if (MusicManager.Instance != null && gameMusic != null)
        {
            MusicManager.Instance.ChangeMusic(gameMusic);
        }
    }
}