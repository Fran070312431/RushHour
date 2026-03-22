using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton para que otros scripts (como los coches) puedan reproducir sonidos fácilmente
    public static AudioManager Instance;

    [Header("Efectos de Sonido")]
    public AudioClip moveSound;        // Sonido al mover un coche
    public AudioClip winSound;         // Sonido al ganar el nivel
    public AudioClip invalidMoveSound; // Sonido de choque o movimiento no permitido

    private AudioSource sfxSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Creamos y configuramos el componente para los efectos (SFX) por código
        // Lo separamos de la música para que no se corten entre ellos
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false; // Los efectos no deben ser infinitos
        sfxSource.volume = 0.7f;
    }

    public void PlayMoveSound()
    {
        // Usamos PlayOneShot para que si movemos muchos coches rápido, los sonidos se solapen bien
        if (moveSound != null)
        {
            sfxSource.PlayOneShot(moveSound);
        }
    }

    public void PlayWinSound()
    {
        // Reproduce el clip de victoria asignado en el inspector
        if (winSound != null)
        {
            sfxSource.PlayOneShot(winSound);
        }
    }

    public void PlayInvalidMoveSound()
    {
        // Sonido de feedback negativo para avisar al jugador de que no puede mover ahí
        if (invalidMoveSound != null)
        {
            sfxSource.PlayOneShot(invalidMoveSound);
        }
    }
}