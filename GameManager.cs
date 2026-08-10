using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Singleton que persiste entre cenas (fases). Guarda quais fases estão
/// desbloqueadas e delega o controle de "fase completa" para cada cena,
/// que avisa via ReportPhaseComplete() quando todos os monumentos
/// daquela fase foram fotografados.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.Serializable]
    public class PhaseInfo
    {
        public string phaseId;      // ex: "Fase01"
        public string sceneName;    // nome exato da Scene no Build Settings
        public bool unlocked;
    }

    [Header("Configuração das fases (na ordem)")]
    public List<PhaseInfo> phases;

    public string CurrentPhaseId { get; private set; }
    public UnityEvent<string> onPhaseCompleted;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // A primeira fase sempre começa desbloqueada
        if (phases.Count > 0) phases[0].unlocked = true;
    }

    public void LoadPhase(string phaseId)
    {
        var phase = phases.Find(p => p.phaseId == phaseId);
        if (phase == null || !phase.unlocked)
        {
            Debug.LogWarning($"Fase {phaseId} não encontrada ou ainda bloqueada.");
            return;
        }
        CurrentPhaseId = phaseId;
        SceneManager.LoadScene(phase.sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Chame isso na cena da fase (ex: dentro de um PhasePOITracker)
    /// quando todos os MonumentPOI daquela fase forem fotografados.
    /// </summary>
    public void ReportPhaseComplete(string phaseId)
    {
        int index = phases.FindIndex(p => p.phaseId == phaseId);
        if (index == -1) return;

        onPhaseCompleted?.Invoke(phaseId);

        // Desbloqueia a próxima fase da lista, se existir
        if (index + 1 < phases.Count)
            phases[index + 1].unlocked = true;
    }

    public bool IsPhaseUnlocked(string phaseId)
    {
        var phase = phases.Find(p => p.phaseId == phaseId);
        return phase != null && phase.unlocked;
    }
}
