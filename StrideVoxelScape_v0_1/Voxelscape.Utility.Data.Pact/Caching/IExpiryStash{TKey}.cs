using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	public interface IExpiryStash<TKey> : IVisiblyDisposable
	{
		void AddExpiration(ExpiryToken<TKey> expiration);

		bool TryExpireToken();
	}

	public static class IExpiryStashContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddExpiration<TKey>(IExpiryStash<TKey> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TryExpireToken<TKey>(IExpiryStash<TKey> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}
	}
}
