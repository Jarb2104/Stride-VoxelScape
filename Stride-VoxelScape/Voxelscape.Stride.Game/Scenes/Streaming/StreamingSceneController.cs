using System;
using System.Linq;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Rasterization;
using Voxelscape.Stages.Management.Core.Interests;
using Voxelscape.Stages.Management.Core.Stages;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Core.Conversions;
using Voxelscape.Utility.Common.Core.Diagnostics;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Stride.Utility.Core.Components;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;

namespace Voxelscape.Stride.Game.Scenes.Streaming
{
	/// <summary>
	///
	/// </summary>
	public class StreamingSceneController : SyncScript
	{
		private readonly DeveloperModeStatus developerMode = new DeveloperModeStatus();

		private readonly VoxelscapeGame game;

		// stage needs to be held onto to keep it from getting garbage collected
		private InterestStage<ChunkKey, bool, Entity> stage;

		public StreamingSceneController(VoxelscapeGame game)
		{
			Contracts.Requires.That(game != null);

			this.game = game;
			this.developerMode.IsActiveChanged.Subscribe(
				isActive => Console.WriteLine($"DeveloperModeStatus.IsActive: {isActive}"));
		}

		private static int ViewDiameterInChunks => 10;

		/// <inheritdoc />
		public override void Start()
		{
			base.Start();

			var observablePosition = new ObservablePosition()
			{
				ToggleEnabledKey = Keys.D1,
				IsEnabled = true,
			};

			var camera = this.SceneSystem.SceneInstance.RootScene.Entities.First(entity => entity.Name == "Camera");
			camera.Add(observablePosition);
			////camera.Transform.Position += new Vector3(0, 0, 0);
			camera.Transform.Position += new Vector3(0, 256, 0);

			this.stage = new InterestStage<ChunkKey, bool, Entity>(
				Factory.From((ChunkKey key) => new Entity()), SingleInterest.Merger);

			////var meshFactory = StreamingStageMeshFactory.CreateCubes();
			////var meshFactory = StreamingStageMeshFactory.CreateCubeOutlines();
			////var meshFactory = StreamingStageMeshFactory.CreateNoise();
			var meshFactory = StreamingStageMeshFactory.CreateSkyIsland();

			var viewDiameter = meshFactory.ChunkLength * ViewDiameterInChunks;
			var converter = new TwoWayTypeConverter<Index3D, ChunkKey>(
				index => new ChunkKey(index), key => key.Index);

			AreaOfInterest.CreateCenteredSpiral(
				new ConverterInterestMap<ChunkKey, Index3D, bool>(this.stage.Interests, converter),
				true,
				observablePosition.PositionChanged.ToMono(),
				new SphereMask<bool>(viewDiameter),
				meshFactory.ChunkLength);

			var stageEntity = new Entity("Stage");
			stageEntity.Add(new StreamingChunkGeneratorScript(this.stage.Chunks, meshFactory));
			this.SceneSystem.SceneInstance.RootScene.Entities.Add(stageEntity);
		}

		/// <inheritdoc />
		public override void Update()
		{
			if (this.Input.IsKeyPressed(Keys.Escape))
			{
				this.game.Exit();
			}

			if (this.Input.IsKeyPressed(Keys.OemTilde))
			{
				this.developerMode.ToggleIsActive();
			}
		}

		/// <inheritdoc />
		public override void Cancel()
		{
			base.Cancel();
			this.developerMode.Dispose();
		}
	}
}
