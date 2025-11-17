namespace SubHorror.Depth
{
	public interface IDepth
	{
		bool Active { get; }
		float DepthPerSecond { get; }
		float DepthContribution();
	}
}