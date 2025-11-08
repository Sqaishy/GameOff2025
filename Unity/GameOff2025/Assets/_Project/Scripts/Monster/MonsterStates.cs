using System;
using Unity.Behavior;

namespace SubHorror.Monster
{
	[BlackboardEnum]
	public enum MonsterStates
	{
		Idle,
		Follow,
		Rage
	}
}