using System;
using System.Reactive.Subjects;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="ISubject{TSource, TResult}"/>.
/// </summary>
public static class ISubjectExtensions
{
	public static void OnCompletedAndDispose<TSource, TResult>(this ISubject<TSource, TResult> subject)
	{
		Contracts.Requires.That(subject != null);

		subject.OnCompleted();
		var disposable = subject as IDisposable;
		disposable?.Dispose();
	}
}
