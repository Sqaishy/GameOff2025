using System;
using SubHorror.Noise;
using UnityEngine;

public class NoiseEmitterTest : MonoBehaviour
{
    [SerializeField] private bool playNoise;
    [SerializeField] private NoiseSettings noiseSettings;

    private const float NoiseTime = 1.5f;
    private NoiseEmitter noiseEmitter;
    private ToggleNoise radioNoise;
    private float currentTime;

    private void Awake()
    {
        noiseEmitter = GetComponent<NoiseEmitter>();
        radioNoise = new ToggleNoise(noiseEmitter, noiseSettings);
        radioNoise.ResetNoise();
        radioNoise.NoisePlaying(playNoise);
    }

    private void Start()
    {
        noiseEmitter.PlayNoise(radioNoise);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= NoiseTime && playNoise)
        {
            Debug.Log($"Total noise for {name} - {noiseEmitter.TotalNoiseLevel()}db");
            currentTime = 0;
        }
    }
}
