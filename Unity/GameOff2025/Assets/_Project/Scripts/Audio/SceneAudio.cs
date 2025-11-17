using UnityEngine;
using FMODUnity;

/// <summary>
/// Simple helper to play scene-specific BGM and ambience on Start.
/// Attach one per scene and assign FMOD EventReference fields.
/// </summary>
public class SceneAudio : MonoBehaviour
{
    [Header("Per-scene audio (FMOD Events)")]
    public EventReference sceneBGM;
    public EventReference sceneAmbience;

    private void Start()
    {
        if (!sceneBGM.IsNull && AudioManager.Instance != null)
            AudioManager.Instance.PlayBGM(sceneBGM);

        if (!sceneAmbience.IsNull && AudioManager.Instance != null)
            AudioManager.Instance.PlayAmbience(sceneAmbience);
    }
}
