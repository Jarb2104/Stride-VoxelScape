namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	///
	/// </summary>
	public interface IPersistenceConfigFactory
	{
		IPersistenceConfig CreateConfig<TEntity>()
			where TEntity : class;
	}
}
