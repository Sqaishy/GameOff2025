using System;
using SubHorror.States;
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