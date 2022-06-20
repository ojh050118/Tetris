using osu.Framework.Platform;
using osu.Framework;
using Tetris.Game;

namespace Tetris.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Tetris"))
            using (osu.Framework.Game game = new TetrisGame())
                host.Run(game);
        }
    }
}
