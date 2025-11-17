using System;

namespace SubHorror.Depth
{
	public class Depth
	{
		private static Depth _instance;
		private DepthController controller;

		public Depth(DepthController controller)
		{
			this.controller = controller;

			if (_instance != null)
				throw new Exception("Trying to create more than one instance of Depth");

			_instance ??= this;
		}

		public static void AddDepth(IDepth depthContributor)
		{
			_instance.controller.AddDepth(depthContributor);
		}

		public static void ToggleDepthClimb(bool canMove)
		{
			_instance.controller.ToggleClimb(canMove);
		}
	}
}