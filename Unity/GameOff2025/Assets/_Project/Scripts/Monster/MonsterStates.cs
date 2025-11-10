using System;
using SubHorror.States;
using Unity.Behavior;
using UnityEngine;

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