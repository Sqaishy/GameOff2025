using System;
using SubHorror.Noise;
using UnityEngine;

public class NoiseEmitterTest : MonoBehaviour
{
    [SerializeField] private bool playNoise;
    [SerializeField] private NoiseSettings noiseSettings;

    private const float NoiseTime = 1.5f;
    private NoiseEmitter noiseEmitter;
    private float currentTime;

    private void Awake()
    {
        noiseEmitter = GetComponent<NoiseEmitter>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= NoiseTime && playNoise)
        {
            noiseEmitter.PlayNoise(noiseSettings);
            Debug.Log($"Total noise for {name} - {noiseEmitter.TotalNoiseLevel()}db");
            currentTime = 0;
        }
    }
}
