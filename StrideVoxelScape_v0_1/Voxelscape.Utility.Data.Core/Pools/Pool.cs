using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	///
	/// </summary>
	public static class Pool
	{
		public static IPool<T> New<T>(IPoolOptions<T> options = null) => new Pool<T>(options);

		public static class WithFactory
		{
			public static IPool<T> New<T>(IFactory<T> factory, IPoolOptions<T> options = null) =>
				new FactoryPool<T>(factory, new Pool<T>(options));

			public static IPool<T> New<T>(IAsyncFactory<T> factory, IPoolOptions<T> options = null) =>
				new AsyncFactoryPool<T>(factory, new Pool<T>(options));

			public static class Bounded
			{
				public static IPool<T> New<T>(IFactory<T> factory, IPoolOptions<T> options = null) =>
					new BoundedFactoryPool<T>(factory, new Pool<T>(options));

				public static IPool<T> New<T>(IAsyncFactory<T> factory, IPoolOptions<T> options = null) =>
					new BoundedAsyncFactoryPool<T>(factory, new Pool<T>(options));
			}
		}

		public static class Tracking
		{
			public static TrackingPool<T> New<T>() => New(new TrackingPoolOptions<T>());

			public static TrackingPool<T> New<T>(TrackingPoolOptions<T> options) =>
				new TrackingPool<T>(Pool.New(options), options);

			public static class WithFactory
			{
				public static TrackingPool<T> New<T>(IFactory<T> factory) =>
					New(factory, new TrackingPoolOptions<T>());

				public static TrackingPool<T> New<T>(IFactory<T> factory, TrackingPoolOptions<T> options) =>
					new TrackingPool<T>(Pool.WithFactory.New(factory, options), options);

				public static TrackingPool<T> New<T>(IAsyncFactory<T> factory) =>
					New(factory, new TrackingPoolOptions<T>());

				public static TrackingPool<T> New<T>(IAsyncFactory<T> factory, TrackingPoolOptions<T> options) =>
					new TrackingPool<T>(Pool.WithFactory.New(factory, options), options);

				public static class Bounded
				{
					public static TrackingPool<T> New<T>(IFactory<T> factory) =>
						New(factory, new TrackingPoolOptions<T>());

					public static TrackingPool<T> New<T>(IFactory<T> factory, TrackingPoolOptions<T> options) =>
						new TrackingPool<T>(Pool.WithFactory.Bounded.New(factory, options), options);

					public static TrackingPool<T> New<T>(IAsyncFactory<T> factory) =>
						New(factory, new TrackingPoolOptions<T>());

					public static TrackingPool<T> New<T>(IAsyncFactory<T> factory, TrackingPoolOptions<T> options) =>
						new TrackingPool<T>(Pool.WithFactory.Bounded.New(factory, options), options);
				}
			}
		}
	}
}
