using UnityEngine;

namespace SubHorror
{
	public class PlayerStateMachineController : MonoBehaviour
	{
		[SerializeField] private PlayerContext playerContext = new();
		private StateMachine machine;

		private void Awake()
		{
			machine = new StateMachineBuilder(new PlayerRoot(null, playerContext)).Build();
		}

		private void Update()
		{
			machine.Update();

			Debug.Log(string.Join(" > ", machine.Root.PathToActiveChild()));
		}
	}
}