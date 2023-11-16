using Voxelscape.Stride.Game;

namespace Stride_VoxelScape;
class Stride_VoxelScapeApp
{
    static void Main(string[] args)
    {
        using var game = new VoxelscapeGame();
        game.Run();
    }
}
