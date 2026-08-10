using UnityEngine;

/// <summary>
/// Objeto independente (escala normal, NÃO filho de objetos com escala
/// gigante) posicionado perto do botão físico. Detecta quando o robô
/// está perto e permite trocar a skybox apertando a tecla de interação.
/// Mantenha isso separado de objetos com Scale muito grande/pequena,
/// já que colliders herdam a escala do pai e podem ficar gigantes ou
/// minúsculos sem querer.
/// </summary>
[RequireComponent(typeof(Collider))]
public class SkyboxKeyPrompt : MonoBehaviour
{
    [Header("Referência")]
    public SkyboxSwitcher skyboxSwitcher;

    [Header("Interação por tecla")]
    public KeyCode interactKey = KeyCode.E;
    public string playerTag = "Player";

    [Header("Texto flutuante (ex: 'Aperte E para mudar o local')")]
    public GameObject promptText;

    [Header("Som de interação")]
    public AudioSource audioSource;
    public AudioClip interactSound;

    private bool isPlayerNearby = false;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Start()
    {
        if (promptText != null) promptText.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            if (audioSource != null && interactSound != null)
                audioSource.PlayOneShot(interactSound);

            if (skyboxSwitcher != null)
                skyboxSwitcher.NextImage();
        }
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
}
