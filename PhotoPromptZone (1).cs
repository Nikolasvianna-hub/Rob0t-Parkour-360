using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Coloque num Collider (Is Trigger = true) no ponto mais alto de cada fase.
/// Quando o robô entra, mostra um prompt simples ("Aperte F"). Ao apertar F
/// dentro da zona, dispara o evento de foto (que o PhotoResultController
/// escuta para fazer o fade e mostrar a imagem).
/// </summary>
[RequireComponent(typeof(Collider))]
public class PhotoPromptZone : MonoBehaviour
{
    [Header("Input")]
    public KeyCode photoKey = KeyCode.F;

    [Header("UI do prompt (ex: um Text/Image dizendo 'Aperte F')")]
    public GameObject promptUI;

    [Header("Player")]
    public string playerTag = "Player";

    public UnityEvent onPhotoTriggered;

    private bool playerInside = false;

    void Start()
    {
        playerInside = false;
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInside = true;
        if (promptUI != null) promptUI.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInside = false;
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(photoKey))
        {
            onPhotoTriggered?.Invoke();
        }
    }
}
