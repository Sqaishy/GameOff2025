using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubHorror.Core
{
	public interface IGameStart
	{
		void GameStart();
	}

	public interface IGameEnd
	{
		void GameEnd();
	}

	public static class GameControl
	{
		public static void GameStart()
		{
			IEnumerable<IGameStart> gameStarts = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
				.OfType<IGameStart>();

			foreach (IGameStart gameStart in gameStarts)
				gameStart.GameStart();
		}

		public static void GameEnd()
		{
			IEnumerable<IGameEnd> gameStarts = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
				.OfType<IGameEnd>();

			foreach (IGameEnd gameEnd in gameStarts)
				gameEnd.GameEnd();
		}
	}
}