using UnityEngine;

/// <summary>
/// Coloque num objeto 3D com Collider (ex: o botão físico na cena).
/// Troca a próxima imagem 360 via SkyboxSwitcher de duas formas:
/// 1) Clicando nele com o mouse (qualquer distância, olhando pra ele).
/// 2) Apertando a tecla de interação (E) quando o robô estiver perto
///    (dentro da zona de proximidade).
///
/// Requer DOIS colliders no mesmo objeto:
/// - Um Collider "sólido" (ex: Box/Mesh Collider, NÃO trigger) do
///   tamanho real do botão, usado pelo clique do mouse.
/// - Um segundo Box Collider marcado como "Is Trigger", maior, usado
///   como zona de proximidade para a tecla E. Precisa de um Rigidbody
///   (Is Kinematic = true) no mesmo objeto para o trigger funcionar,
///   já que o CharacterController do robô não tem Rigidbody próprio.
/// </summary>
[RequireComponent(typeof(Collider))]
public class SkyboxButtonInteractable : MonoBehaviour
{
    [Header("Referência")]
    public SkyboxSwitcher skyboxSwitcher;

    [Header("Interação por tecla (quando o robô está perto)")]
    public KeyCode interactKey = KeyCode.E;
    public string playerTag = "Player";

    [Header("Texto flutuante (ex: 'Aperte E para mudar o local')")]
    public GameObject promptText;

    [Header("Feedback visual simples ao passar o mouse")]
    public float hoverScaleMultiplier = 1.1f;
    private Vector3 baseScale;
    private bool isHoveringMouse = false;
    private bool isPlayerNearby = false;

    void Start()
    {
        baseScale = transform.localScale;
        if (promptText != null) promptText.SetActive(false);
    }

    void Update()
    {
        // Efeito leve de "respirar" enquanto o mouse está em cima
        float targetScale = isHoveringMouse ? hoverScaleMultiplier : 1f;
        transform.localScale = Vector3.Lerp(transform.localScale, baseScale * targetScale, Time.deltaTime * 8f);

        // Interação por tecla, só funciona se o robô estiver na zona de proximidade
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            TriggerSwitch();
        }
    }

    void OnMouseEnter()
    {
        isHoveringMouse = true;
    }

    void OnMouseExit()
    {
        isHoveringMouse = false;
    }

    void OnMouseDown()
    {
        TriggerSwitch();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        isPlayerNearby = true;
        if (promptText != null) promptText.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        isPlayerNearby = false;
        if (promptText != null) promptText.SetActive(false);
    }

    private void TriggerSwitch()
    {
        if (skyboxSwitcher != null)
            skyboxSwitcher.NextImage();
    }
}
