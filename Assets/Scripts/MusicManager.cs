using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Música")]
    public AudioClip menuMusic;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton: Solo puede existir uno
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // NO destruir al cambiar de escena

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.5f;

            if (menuMusic != null)
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject); // Si ya existe uno, destruir este
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource != null && newClip != null)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}