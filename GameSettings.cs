using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton persistente (sobrevive entre cenas) que guarda as
/// configurações do jogador (brilho e volume), salva em PlayerPrefs,
/// e reaplica o brilho automaticamente toda vez que uma cena nova
/// carrega (já que o overlay de brilho existe em cada cena, não é
/// persistente).
/// </summary>
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    [Range(0f, 1f)] public float brightness = 1f; // 1 = normal, 0 = tela escura
    [Range(0f, 1f)] public float volume = 1f;

    private const string BrightnessKey = "settings_brightness";
    private const string VolumeKey = "settings_volume";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        brightness = PlayerPrefs.GetFloat(BrightnessKey, 1f);
        volume = PlayerPrefs.GetFloat(VolumeKey, 1f);

        ApplyVolume();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyBrightnessToScene();
    }

    public void SetBrightness(float value)
    {
        brightness = value;
        PlayerPrefs.SetFloat(BrightnessKey, brightness);
        ApplyBrightnessToScene();
    }

    public void SetVolume(float value)
    {
        volume = value;
        PlayerPrefs.SetFloat(VolumeKey, volume);
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        AudioListener.volume = volume;
    }

    private void ApplyBrightnessToScene()
    {
        var overlay = FindFirstObjectByType<BrightnessOverlay>();
        if (overlay != null)
            overlay.SetBrightness(brightness);
    }
}
