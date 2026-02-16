using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Efectos de Sonido")]
    public AudioClip moveSound;
    public AudioClip winSound;
    public AudioClip invalidMoveSound;

    private AudioSource sfxSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Solo crear AudioSource para efectos (NO música)
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 0.7f;
    }

    public void PlayMoveSound()
    {
        if (moveSound != null)
        {
            sfxSource.PlayOneShot(moveSound);
        }
    }

    public void PlayWinSound()
    {
        if (winSound != null)
        {
            sfxSource.PlayOneShot(winSound);
        }
    }

    public void PlayInvalidMoveSound()
    {
        if (invalidMoveSound != null)
        {
            sfxSource.PlayOneShot(invalidMoveSound);
        }
    }
}