using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Indexables
{
	/// <summary>
	/// Wraps another indexable instance, projecting from its source type to a different result type. This allows
	/// an indexable instance of one type to be viewed as an indexable instance of a different type while still
	/// being the same instance.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TSource">The type of the source to project from.</typeparam>
	/// <typeparam name="TResult">The type of the result to project the source to.</typeparam>
	public class ProjectionIndexable<TIndex, TSource, TResult> : IIndexable<TIndex, TResult>
		where TIndex : IIndex
	{
		/// <summary>
		/// The source indexable used to back this projection to another type of indexable.
		/// </summary>
		private readonly IIndexable<TIndex, TSource> indexable;

		/// <summary>
		/// The converter used to convert from the source type to the result type.
		/// </summary>
		private readonly Func<IIndexable<TIndex, TSource>, TIndex, TSource, TResult> toResultConverter;

		/// <summary>
		/// The action to execute to store the result type value in the source indexable.
		/// </summary>
		private readonly Action<IIndexable<TIndex, TSource>, TIndex, TResult> toSourceAction;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectionIndexable{TIndex, TSource, TResult}" /> class.
		/// </summary>
		/// <param name="sourceIndexable">The source indexable.</param>
		/// <param name="toResultConverter">The converter used to convert from the source type to the result type.</param>
		/// <param name="toSourceAction">The action to execute to store the result type value in the source indexable.</param>
		public ProjectionIndexable(
			IIndexable<TIndex, TSource> sourceIndexable,
			Func<IIndexable<TIndex, TSource>, TIndex, TSource, TResult> toResultConverter,
			Action<IIndexable<TIndex, TSource>, TIndex, TResult> toSourceAction)
		{
			Contracts.Requires.That(sourceIndexable != null);
			Contracts.Requires.That(toResultConverter != null);
			Contracts.Requires.That(toSourceAction != null);

			this.indexable = sourceIndexable;
			this.toResultConverter = toResultConverter;
			this.toSourceAction = toSourceAction;
		}

		#region IIndexable<TIndex,TResultValue> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return this.indexable.Rank; }
		}

		/// <inheritdoc />
		public TResult this[TIndex index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				return this.toResultConverter(this.indexable, index, this.indexable[index]);
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.toSourceAction(this.indexable, index, value);
			}
		}

		/// <inheritdoc />
		public bool IsIndexValid(TIndex index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return this.indexable.IsIndexValid(index);
		}

		/// <inheritdoc />
		public bool TryGetValue(TIndex index, out TResult value)
		{
			IReadOnlyIndexableContracts.TryGetValue(this, index);

			TSource sourceValue;
			if (this.indexable.TryGetValue(index, out sourceValue))
			{
				value = this.toResultConverter(this.indexable, index, sourceValue);
				return true;
			}
			else
			{
				value = default(TResult);
				return false;
			}
		}

		/// <inheritdoc />
		public bool TrySetValue(TIndex index, TResult value)
		{
			IIndexableContracts.TrySetValue(this, index);

			if (this.indexable.IsIndexValid(index))
			{
				this.toSourceAction(this.indexable, index, value);
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region IEnumerable<KeyValuePair<TIndex,TResultValue>> Members

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TIndex, TResult>> GetEnumerator()
		{
			return this.indexable.Select(pair => new KeyValuePair<TIndex, TResult>(
				pair.Key, this.toResultConverter(this.indexable, pair.Key, pair.Value))).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
