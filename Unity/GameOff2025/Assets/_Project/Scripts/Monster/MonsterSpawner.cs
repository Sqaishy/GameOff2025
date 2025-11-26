using SubHorror.Core;
using UnityEngine;

namespace SubHorror.Monster
{
	public class MonsterSpawner : MonoBehaviour, IGameStart
	{
		[SerializeField] private MonsterStateMachineController monster;

		private MonsterStateMachineController spawnedMonster;

		public MonsterStateMachineController ActiveMonster => spawnedMonster;

		public void DisableMonster() => spawnedMonster.gameObject.SetActive(false);

		public void GameStart()
		{
			spawnedMonster = Instantiate(monster, transform.position, Quaternion.identity);
		}
	}
}