namespace Voxelscape.Stages.Management.Pact.Tasks
{
	public interface IVoxelTask<TView, TResult>
	{
		TResult Run(TView view);
	}
}
