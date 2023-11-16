using System.IO;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	/// <summary>
	///
	/// </summary>
	public class PartitionedConfigFactory : IPersistenceConfigFactory
	{
		private readonly string directoryPath;

		private readonly string fileNamePrefix;

		private readonly string fileNameSuffix;

		public PartitionedConfigFactory(string directoryPath, string fileNamePrefix, string fileNameSuffix)
		{
			Contracts.Requires.That(!directoryPath.IsNullOrWhiteSpace());
			Contracts.Requires.That(fileNamePrefix != null);
			Contracts.Requires.That(fileNameSuffix != null);

			this.directoryPath = directoryPath;
			this.fileNamePrefix = fileNamePrefix;
			this.fileNameSuffix = fileNameSuffix;
		}

		/// <inheritdoc />
		public IPersistenceConfig CreateConfig<TEntity>()
			where TEntity : class => new PersistenceConfig(Path.Combine(
				this.directoryPath,
				this.fileNamePrefix + typeof(TEntity).Name + this.fileNameSuffix));
	}
}
