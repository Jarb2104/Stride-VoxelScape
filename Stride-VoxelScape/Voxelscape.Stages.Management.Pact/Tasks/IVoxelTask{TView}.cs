namespace Voxelscape.Stages.Management.Pact.Tasks
{
	public interface IVoxelTask<TView>
	{
		void Run(TView view);
	}
}
