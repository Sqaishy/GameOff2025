using System;
using SubHorror.Noise;
using UnityEngine;

namespace SubHorror
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        private NoiseEmitter noiseEmitter;

        private void Awake()
        {
            noiseEmitter = GetComponentInChildren<NoiseEmitter>();
        }

        public void EmitNoise(NoiseSettings noiseSettings) => noiseEmitter.PlayNoise(noiseSettings);
    }
}