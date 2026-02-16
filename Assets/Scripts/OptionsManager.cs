using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject optionsPanel;
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public Toggle fullscreenToggle; // NUEVO

    void Start()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }

        LoadSettings();

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // NUEVO: Conectar toggle de pantalla completa
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);
        }
    }

    public void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;

        if (volumeValueText != null)
        {
            volumeValueText.text = Mathf.RoundToInt(value * 100) + "%";
        }

        SaveSettings();
    }

    // NUEVO
    void OnFullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        if (volumeValueText != null)
        {
            volumeValueText.text = Mathf.RoundToInt(savedVolume * 100) + "%";
        }

        // NUEVO: Cargar pantalla completa
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = isFullscreen;
    }
}