using Stride.Engine;
using Stride.Games;
using Voxelscape.Stride.Game.Scenes.Streaming;

namespace Voxelscape.Stride.Game
{
	/// <summary>
	///
	/// </summary>
	public class VoxelscapeGame : global::Stride.Engine.Game
	{
		private bool updatedOnce = false;

		/// <inheritdoc />
		protected override void BeginRun()
		{
			base.BeginRun();
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!updatedOnce)
			{
				var sceneController = new Entity(nameof(StreamingSceneController));
				sceneController.Add(new StreamingSceneController(this));
				SceneSystem.SceneInstance.RootScene.Entities.Add(sceneController);
			}
			updatedOnce = true;
		}
	}
}
