using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Singleton para que solo haya un gestor de música en todo el juego
    public static MusicManager Instance;

    [Header("Música")]
    public AudioClip menuMusic; // El archivo de audio de la música principal

    private AudioSource audioSource;

    void Awake()
    {
        // Lógica de Singleton: si no hay ninguna instancia, nos quedamos con esta
        if (Instance == null)
        {
            Instance = this;

            // Hacemos que el objeto persista entre escenas para que la música sea continua
            DontDestroyOnLoad(gameObject);

            // Configuramos el componente AudioSource por código
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true; // Que la música se repita en bucle
            audioSource.volume = 0.5f;

            // Si hemos asignado un clip de audio, lo reproducimos al empezar
            if (menuMusic != null)
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }
        else
        {
            // Si ya existe un gestor de música, destruimos este nuevo para no tener música duplicada
            Destroy(gameObject);
        }
    }

    // Función para ajustar el volumen de la música (útil para el menú de opciones)
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    // Función para detener la reproducción por completo
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // Función para reanudar o empezar a tocar la música si estaba parada
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Función para cambiar la canción actual por una nueva de forma dinámica
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