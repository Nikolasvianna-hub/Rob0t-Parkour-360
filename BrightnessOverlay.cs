using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Coloque numa Image preta full-screen (sempre ativa), em CADA cena
/// (menu, pausa, fases). O GameSettings encontra esse componente e
/// ajusta o Alpha dela: brightness=1 -> Alpha 0 (tela normal),
/// brightness=0 -> Alpha ~0.9 (tela bem escura).
/// </summary>
public class BrightnessOverlay : MonoBehaviour
{
    public Image overlayImage;
    [Range(0f, 1f)] public float maxDarkness = 0.85f; // nunca fica 100% preto

    void Start()
    {
        if (GameSettings.Instance != null)
            SetBrightness(GameSettings.Instance.brightness);
    }

    public void SetBrightness(float brightness)
    {
        if (overlayImage == null) return;
        Color c = overlayImage.color;
        c.a = Mathf.Lerp(maxDarkness, 0f, brightness);
        overlayImage.color = c;
    }
}
