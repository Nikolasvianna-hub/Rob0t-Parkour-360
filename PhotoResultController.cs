using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Escuta o PhotoPromptZone.onPhotoTriggered: dispara um flash branco +
/// som de câmera, depois faz fade pra preto, mostra a imagem do lugar,
/// um texto contando sobre o local e o botão de avançar para a próxima
/// fase.
/// </summary>
public class PhotoResultController : MonoBehaviour
{
    [Header("Referências de UI")]
    public CanvasGroup fadeOverlay;      // painel preto full-screen
    public GameObject resultPanel;       // painel com a Image da foto + texto + botão
    public Image photoDisplay;           // Image dentro do resultPanel que recebe a foto
    public TMP_Text locationText;        // texto que conta sobre o local

    [Header("Flash de captura")]
    public Image flashImage;             // Image branca full-screen, Alpha = 0 por padrão
    public float flashInDuration = 0.05f;
    public float flashOutDuration = 0.2f;

    [Header("Som de captura")]
    public AudioSource audioSource;      // AudioSource no mesmo objeto (ou arraste um existente)
    public AudioClip shutterSound;       // seu som de clique de câmera

    [Header("Conteúdo desta fase")]
    public Sprite phasePhoto;            // a foto do lugar (arraste o Sprite pronto)
    [TextArea(3, 6)]
    public string locationDescription;   // texto sobre o local (nome + curiosidade)

    [Header("Timing")]
    public float fadeDuration = 0.6f;

    [Header("Trava movimento enquanto mostra a foto")]
    public MonoBehaviour[] scriptsToDisableWhileShowing; // ex: FirstPersonController, StarterAssetsInputs

    [Header("Atalho de teclado (opcional)")]
    public KeyCode nextPhaseKey = KeyCode.Return;
    public string nextSceneNameForKeyShortcut; // preencha se quiser usar o atalho

    private bool isShowingResult = false;

    void Start()
    {
        if (resultPanel != null) resultPanel.SetActive(false);
        if (fadeOverlay != null) fadeOverlay.alpha = 0f;
        if (flashImage != null) SetImageAlpha(flashImage, 0f);
    }

    void Update()
    {
        if (isShowingResult && !string.IsNullOrEmpty(nextSceneNameForKeyShortcut) && Input.GetKeyDown(nextPhaseKey))
        {
            GoToNextPhase(nextSceneNameForKeyShortcut);
        }
    }

    // Ligue este método no evento PhotoPromptZone.onPhotoTriggered pelo Inspector
    public void ShowPhotoResult()
    {
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        SetScriptsEnabled(false);

        // Flash + som de captura, antes de qualquer coisa
        if (audioSource != null && shutterSound != null)
            audioSource.PlayOneShot(shutterSound);

        if (flashImage != null)
            yield return FlashRoutine();

        yield return Fade(0f, 1f);

        if (photoDisplay != null) photoDisplay.sprite = phasePhoto;
        if (locationText != null) locationText.text = locationDescription;
        if (resultPanel != null) resultPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isShowingResult = true;

        yield return Fade(1f, 0f);
    }

    private IEnumerator FlashRoutine()
    {
        float t = 0f;
        while (t < flashInDuration)
        {
            t += Time.deltaTime;
            SetImageAlpha(flashImage, Mathf.Lerp(0f, 1f, t / flashInDuration));
            yield return null;
        }
        SetImageAlpha(flashImage, 1f);

        t = 0f;
        while (t < flashOutDuration)
        {
            t += Time.deltaTime;
            SetImageAlpha(flashImage, Mathf.Lerp(1f, 0f, t / flashOutDuration));
            yield return null;
        }
        SetImageAlpha(flashImage, 0f);
    }

    private void SetImageAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }

    // Ligue este método no OnClick do botão "Próxima Fase".
    // O parâmetro é o NOME EXATO da cena da próxima fase
    // (precisa estar adicionada em File > Build Settings > Scenes In Build).
    public void GoToNextPhase(string nextSceneName)
    {
        StartCoroutine(GoToNextRoutine(nextSceneName));
    }

    private IEnumerator GoToNextRoutine(string nextSceneName)
    {
        isShowingResult = false;
        yield return Fade(0f, 1f);
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float from, float to)
    {
        if (fadeOverlay == null) yield break;

        float t = 0f;
        fadeOverlay.alpha = from;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = to;
    }

    private void SetScriptsEnabled(bool enabled)
    {
        foreach (var s in scriptsToDisableWhileShowing)
            if (s != null) s.enabled = enabled;
    }
}
