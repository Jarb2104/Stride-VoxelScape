using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	[DebuggerDisplay("Pinned Keys Count = {PinnedKeys.Count}")]
	[DebuggerTypeProxy(typeof(EnumerableDebugView<>))]
	public class CacheCompletionException<TKey> : InvalidOperationException, IEnumerable<TKey>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CacheCompletionException{TKey}" /> class.
		/// </summary>
		/// <param name="pinnedKeys">The keys of values that are still pinned.</param>
		public CacheCompletionException(IReadOnlyCollection<TKey> pinnedKeys)
		{
			Contracts.Requires.That(pinnedKeys.AllAndSelfNotNull());

			this.PinnedKeys = pinnedKeys;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheCompletionException{TKey}"/> class.
		/// </summary>
		/// <param name="pinnedKeys">The keys of values that are still pinned.</param>
		/// <param name="message">The message that describes the error.</param>
		public CacheCompletionException(IReadOnlyCollection<TKey> pinnedKeys, string message)
			: base(message)
		{
			Contracts.Requires.That(pinnedKeys.AllAndSelfNotNull());

			this.PinnedKeys = pinnedKeys;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheCompletionException{TKey}"/> class.
		/// </summary>
		/// <param name="pinnedKeys">The keys of values that are still pinned.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception,
		/// or a null reference if no inner exception is specified.
		/// </param>
		public CacheCompletionException(
			IReadOnlyCollection<TKey> pinnedKeys, string message, Exception innerException)
			: base(message, innerException)
		{
			Contracts.Requires.That(pinnedKeys.AllAndSelfNotNull());

			this.PinnedKeys = pinnedKeys;
		}

		/// <summary>
		/// Gets the keys of values that were still pinned in the cache.
		/// </summary>
		/// <value>
		/// The keys of still pinned values.
		/// </value>
		/// <remarks>
		/// Values that remained pinned in the cache when trying to complete the cache prevent it from
		/// being completed because those values cannot be safely released as they may still be in use
		/// by whatever is holding their pins. All values must be unpinned in order to complete the cache.
		/// </remarks>
		public IReadOnlyCollection<TKey> PinnedKeys { get; }

		/// <inheritdoc />
		public IEnumerator<TKey> GetEnumerator() => this.PinnedKeys.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
