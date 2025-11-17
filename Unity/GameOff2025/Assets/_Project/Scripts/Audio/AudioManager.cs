using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

/// <summary>
/// Singleton AudioManager for FMOD in Unity.
/// Supports: 2D/3D one-shots, looped SFX (attach/position/2D), scene BGM & ambience, and bus volume control.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("FMOD Buses (paths must match your FMOD project)")]
    [SerializeField] private string bgmBusPath = "bus:/BGM";
    [SerializeField] private string sfxBusPath = "bus:/SFX";
    [SerializeField] private string ambBusPath = "bus:/AMB";
    [SerializeField] private string masterBusPath = "bus:/Master";




    // FMOD objects
    private Bus masterBus;
    private Bus bgmBus;
    private Bus sfxBus;
    private Bus ambBus;

    private EventInstance bgmInstance;
    private EventInstance ambInstance;

    // Active looped SFX we manage (stopped on scene load)
    private readonly List<EventInstance> activeLoops = new List<EventInstance>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

      

        // Cache buses
        bgmBus = RuntimeManager.GetBus(bgmBusPath);
        sfxBus = RuntimeManager.GetBus(sfxBusPath);
        ambBus = RuntimeManager.GetBus(ambBusPath);

        // Subscribe to sceneLoaded to auto-clean looped SFX
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe event
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

        // Stop and release bgm/amb and loops to be safe
        StopAndReleaseSafe(bgmInstance);
        StopAndReleaseSafe(ambInstance);
        StopAllLoopsImmediate();
    }

    // -----------------------
    // Bank loading (optional)
    // -----------------------


    // -----------------------
    // One-shot SFX
    // -----------------------
    /// <summary>Play a 2D one-shot SFX (non-spatial).</summary>
    public void PlayOneShot2D(EventReference sfxEvent)
    {
        if (sfxEvent.IsNull) return;
        RuntimeManager.PlayOneShot(sfxEvent);
    }

    /// <summary>Play a 3D one-shot SFX at position (spatial).</summary>
    public void PlayOneShot3D(EventReference sfxEvent, Vector3 position)
    {
        if (sfxEvent.IsNull) return;
        RuntimeManager.PlayOneShot(sfxEvent, position);
    }

    // -----------------------
    // Looped SFX (returns EventInstance so caller can control it)
    // -----------------------
    /// <summary>Create and start a looped 2D SFX (no spatialization). Returned instance must be stopped/released by caller or will be auto-stopped on scene load.</summary>
    public EventInstance PlayLoop2D(EventReference sfxEvent)
    {
        if (sfxEvent.IsNull) return default;
        var inst = RuntimeManager.CreateInstance(sfxEvent);
        inst.start();
        activeLoops.Add(inst);
        return inst;
    }

    /// <summary>Create and start a looped 3D SFX at a fixed world position. Not attached to an object.</summary>
    public EventInstance PlayLoop3DAtPosition(EventReference sfxEvent, Vector3 position)
    {
        if (sfxEvent.IsNull) return default;
        var inst = RuntimeManager.CreateInstance(sfxEvent);
        inst.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        inst.start();
        activeLoops.Add(inst);
        return inst;
    }

    /// <summary>Create and start a looped 3D SFX attached to a GameObject (follows it). Pass the target GameObject and optionally its Rigidbody (can be null).</summary>
    public EventInstance PlayLoopAttached(EventReference sfxEvent, GameObject target, Rigidbody targetRigidbody = null)
    {
        if (sfxEvent.IsNull || target == null) return default;
        var inst = RuntimeManager.CreateInstance(sfxEvent);
        RuntimeManager.AttachInstanceToGameObject(inst, target.transform, targetRigidbody);
        inst.start();
        activeLoops.Add(inst);
        return inst;
    }

    /// <summary>Stop and release a loop instance. If immediate true, stops immediately; otherwise allows fade-out.</summary>
    public void StopLoop(EventInstance instance, bool immediate = false)
    {
        if (!instance.isValid()) return;

        instance.stop(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        activeLoops.Remove(instance);
    }

    private void StopAllLoopsImmediate()
    {
        for (int i = activeLoops.Count - 1; i >= 0; i--)
        {
            var inst = activeLoops[i];
            if (inst.isValid())
            {
                inst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                inst.release();
            }
        }

        activeLoops.Clear();
    }

    // -----------------------
    // BGM & Ambience
    // -----------------------
    /// <summary>Play/replace background music. Stops previous BGM with fade-out.</summary>
    public void PlayBGM(EventReference musicEvent)
    {
        if (musicEvent.IsNull) return;

        if (bgmInstance.isValid())
        {
            bgmInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bgmInstance.release();
        }

        bgmInstance = RuntimeManager.CreateInstance(musicEvent);
        bgmInstance.start();
    }

    /// <summary>Play/replace ambience. Stops previous ambience with fade-out.</summary>
    public void PlayAmbience(EventReference ambEvent)
    {
        if (ambEvent.IsNull) return;

        if (ambInstance.isValid())
        {
            ambInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambInstance.release();
        }

        ambInstance = RuntimeManager.CreateInstance(ambEvent);
        ambInstance.start();
    }

    // -----------------------
    // Scene load handling
    // -----------------------
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop all active looped SFX to avoid "ghost" sounds persisting between scenes.
        StopAllLoopsImmediate();
        // Note: we intentionally do not stop BGM/Ambience here so those can persist across scenes
        // unless you want them to be swapped per-scene via SceneAudio (see helper below).
    }

    // -----------------------
    // Bus control (amplitude/volume)
    // -----------------------
    /// <summary>Set BGM bus volume (linear 0..1)</summary>
    public void SetMasterVolume(float linear0to1)
    {
        linear0to1 = Mathf.Clamp01(linear0to1);
        masterBus.setVolume(linear0to1);
    }
    public void SetBGMVolume(float linear0to1)
    {
        linear0to1 = Mathf.Clamp01(linear0to1);
        bgmBus.setVolume(linear0to1);
    }

    /// <summary>Set SFX bus volume (linear 0..1)</summary>
    public void SetSFXVolume(float linear0to1)
    {
        linear0to1 = Mathf.Clamp01(linear0to1);
        sfxBus.setVolume(linear0to1);
    }

    /// <summary>Set Ambience bus volume (linear 0..1)</summary>
    public void SetAMBVolume(float linear0to1)
    {
        linear0to1 = Mathf.Clamp01(linear0to1);
        ambBus.setVolume(linear0to1);
    }

    // -----------------------
    // Utilities
    // -----------------------
    private void StopAndReleaseSafe(EventInstance inst)
    {
        if (!inst.isValid()) return;
        inst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        inst.release();
    }
}
