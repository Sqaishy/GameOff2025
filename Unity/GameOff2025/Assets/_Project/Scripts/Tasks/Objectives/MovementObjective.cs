using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Movement Objective")]
	public class MovementObjective : Objective
	{
		private PlayerStateMachineController controller;
		private bool upPressed;
		private bool downPressed;
		private bool leftPressed;
		private bool rightPressed;

		public override Task.Status Enter()
		{
			controller = Owner.GetComponent<PlayerStateMachineController>();

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			switch (controller.PlayerContext.movement.x)
			{
				case > 0:
					rightPressed = true;
					break;
				case < 0:
					leftPressed = true;
					break;
			}

			switch (controller.PlayerContext.movement.y)
			{
				case > 0:
					upPressed = true;
					break;
				case < 0:
					downPressed = true;
					break;
			}

			return AllInputPressed() ? Task.Status.Success : Task.Status.Running;
		}

		public override void Exit()
		{
			upPressed = false;
			downPressed = false;
			leftPressed = false;
			rightPressed = false;
		}

		private bool AllInputPressed()
		{
			return upPressed && downPressed && leftPressed && rightPressed;
		}
	}
}