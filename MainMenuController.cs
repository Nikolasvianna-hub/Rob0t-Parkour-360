using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controlador do Main Menu. Botões: Jogar (carrega a primeira fase),
/// Opções (abre painel de brilho/volume), Sair (fecha o jogo).
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Cena da primeira fase")]
    public string firstPhaseSceneName = "Fase 1 Unifor";

    [Header("Painéis")]
    public GameObject optionsPanel;

    [Header("Sliders de opções (ligue aqui, opcional)")]
    public Slider brightnessSlider;
    public Slider volumeSlider;

    void Start()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);

        // Sincroniza os sliders com o valor salvo, sem disparar eventos
        if (GameSettings.Instance != null)
        {
            if (brightnessSlider != null) brightnessSlider.SetValueWithoutNotify(GameSettings.Instance.brightness);
            if (volumeSlider != null) volumeSlider.SetValueWithoutNotify(GameSettings.Instance.volume);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(firstPhaseSceneName);
    }

    public void OpenOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Ligue estes métodos nos eventos OnValueChanged(float) dos sliders
    public void OnBrightnessChanged(float value)
    {
        if (GameSettings.Instance != null) GameSettings.Instance.SetBrightness(value);
    }

    public void OnVolumeChanged(float value)
    {
        if (GameSettings.Instance != null) GameSettings.Instance.SetVolume(value);
    }
}
