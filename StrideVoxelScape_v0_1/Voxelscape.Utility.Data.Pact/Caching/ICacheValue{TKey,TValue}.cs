using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	public interface ICacheValue<TKey, TValue> : IKeyed<TKey>, IDisposableValue<TValue>
	{
		void AddExpiration(ExpiryToken<TKey> expiration);
	}

	public static class ICacheValueContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddExpiration<TKey, TValue>(ICacheValue<TKey, TValue> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}
	}
}
