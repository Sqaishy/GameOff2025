using SubHorror.Core;
using UnityEngine;

namespace SubHorror.Monster
{
	public class MonsterSpawner : MonoBehaviour, IGameStart
	{
		[SerializeField] private MonsterStateMachineController monster;
		
		public void GameStart()
		{
			Instantiate(monster, transform.position, Quaternion.identity);
		}
	}
}