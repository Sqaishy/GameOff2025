using System.Collections.Generic;
using System.Linq;
using SubHorror.Core;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Tutorial/Begin Game Consequence")]
	public class BeginGameConsequence : Consequence
	{
		public override void Apply(Task failedTask)
		{
			IEnumerable<IGameStart> gameStarts = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
				.OfType<IGameStart>();

			foreach (IGameStart gameStart in gameStarts)
				gameStart.GameStart();
		}
	}
}