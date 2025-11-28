using SubHorror.Noise;
using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Play Noise Action")]
	public class PlayNoiseContextAction : ContextAction
	{
		[SerializeField] private NoiseSettings noiseSettings;

		public override void Execute(GameObject interactor, GameObject target)
		{
			if (!interactor.TryGetComponent(out NoiseEmitter noiseEmitter))
				return;

			noiseEmitter.PlayNoise(new SingleNoise(noiseEmitter, noiseSettings));
		}
	}
}