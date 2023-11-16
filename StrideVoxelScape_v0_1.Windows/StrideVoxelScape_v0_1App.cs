using Voxelscape.Stride.Game;

namespace StrideVoxelScape_v0_1
{
    class StrideVoxelScape_v0_1App
    {
        static void Main(string[] args)
        {
            using (var game = new VoxelscapeGame())
            {
                game.Run();
            }
        }
    }
}
