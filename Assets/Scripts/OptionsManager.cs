using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject optionsPanel;
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public Toggle fullscreenToggle; // Referencia al interruptor de pantalla completa

    void Start()
    {
        // Al iniciar, el panel de opciones debe estar cerrado por defecto
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }

        // Cargamos la configuración que el usuario guardó la última vez
        LoadSettings();

        // Escuchamos cuando el usuario mueve la barra de volumen
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // Configuramos el toggle de pantalla completa y detectamos sus cambios
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);
        }
    }

    public void OpenOptions()
    {
        // Función para mostrar el menú de ajustes
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        // Función para ocultar el menú de ajustes
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    void OnVolumeChanged(float value)
    {
        // Ajustamos el volumen general del juego según el valor del slider
        AudioListener.volume = value;

        // Actualizamos el porcentaje de texto que ve el usuario (0% a 100%)
        if (volumeValueText != null)
        {
            volumeValueText.text = Mathf.RoundToInt(value * 100) + "%";
        }

        // Guardamos el cambio automáticamente
        SaveSettings();
    }

    void OnFullscreenToggle(bool isFullscreen)
    {
        // Cambiamos entre modo ventana y pantalla completa
        Screen.fullScreen = isFullscreen;

        // Guardamos la preferencia: 1 para sí, 0 para no
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    void SaveSettings()
    {
        // Guardamos el valor decimal del volumen en la memoria local
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        // Intentamos recuperar el volumen guardado; si no hay nada, ponemos 1 (máximo)
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = savedVolume;

        // Sincronizamos el slider y el texto con el valor recuperado
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        if (volumeValueText != null)
        {
            volumeValueText.text = Mathf.RoundToInt(savedVolume * 100) + "%";
        }

        // Recuperamos el estado de pantalla completa (por defecto activado)
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = isFullscreen;
    }
}