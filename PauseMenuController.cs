using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Coloque num objeto sempre ativo dentro de cada fase. Abre/fecha
/// o menu de pausa com ESC, pausando o tempo do jogo (Time.timeScale).
/// Reaproveita o mesmo painel de opções (brilho/volume) do Main Menu.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject pausePanel;
    public GameObject optionsPanel;

    [Header("Cena do menu principal")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Sliders de opções (ligue aqui, opcional)")]
    public Slider brightnessSlider;
    public Slider volumeSlider;

    [Header("Trava a navegação/olhar enquanto pausado")]
    public MonoBehaviour[] scriptsToDisableWhilePaused; // ex: FirstPersonController, StarterAssetsInputs

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null) pausePanel.SetActive(true);
        SetScriptsEnabled(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameSettings.Instance != null)
        {
            if (brightnessSlider != null) brightnessSlider.SetValueWithoutNotify(GameSettings.Instance.brightness);
            if (volumeSlider != null) volumeSlider.SetValueWithoutNotify(GameSettings.Instance.volume);
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        SetScriptsEnabled(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // importante: resetar antes de trocar de cena
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnBrightnessChanged(float value)
    {
        if (GameSettings.Instance != null) GameSettings.Instance.SetBrightness(value);
    }

    public void OnVolumeChanged(float value)
    {
        if (GameSettings.Instance != null) GameSettings.Instance.SetVolume(value);
    }

    private void SetScriptsEnabled(bool enabled)
    {
        foreach (var s in scriptsToDisableWhilePaused)
            if (s != null) s.enabled = enabled;
    }
}
