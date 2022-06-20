using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace Tetris.Game.Tests.Visual
{
    public class TestSceneTetrisGame : TetrisTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        private TetrisGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new TetrisGame();
            game.SetHost(host);

            AddGame(game);
        }
    }
}
