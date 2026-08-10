using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Permite trocar entre 2 ou mais imagens 360 (skyboxes) dentro da mesma
/// fase, via teclado (setas) e via clique do mouse (botão de UI).
/// Cumpre o requisito obrigatório de "navegação entre imagens via
/// teclado" e "via clique do mouse", sem depender do sistema completo
/// de nós/hotspots usado anteriormente.
/// </summary>
public class SkyboxSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct PanoramaImage
    {
        public string label;          // nome do ponto (ex: "Entrada", "Jardim")
        public Material skyboxMaterial;
    }

    [Header("Imagens navegáveis desta fase")]
    public PanoramaImage[] images;

    [Header("Input (teclado)")]
    public KeyCode nextKey = KeyCode.RightArrow;
    public KeyCode previousKey = KeyCode.LeftArrow;

    [Header("Evento (ligue um Text/TMP na UI, opcional)")]
    public UnityEvent<string> onImageChanged; // dispara o label da imagem atual

    private int currentIndex = 0;

    void Start()
    {
        if (images.Length > 0)
            ApplyImage(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(nextKey))
            NextImage();

        if (Input.GetKeyDown(previousKey))
            PreviousImage();
    }

    // Ligue este método no OnClick de um botão de UI (seta "próxima imagem")
    public void NextImage()
    {
        if (images.Length == 0) return;
        currentIndex = (currentIndex + 1) % images.Length;
        ApplyImage(currentIndex);
    }

    // Ligue este método no OnClick de um botão de UI (seta "imagem anterior")
    public void PreviousImage()
    {
        if (images.Length == 0) return;
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        ApplyImage(currentIndex);
    }

    private void ApplyImage(int index)
    {
        var img = images[index];
        RenderSettings.skybox = img.skyboxMaterial;
        DynamicGI.UpdateEnvironment();
        onImageChanged?.Invoke(img.label);
    }
}
