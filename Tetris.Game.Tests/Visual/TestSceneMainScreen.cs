using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetris.Game.Screens;

namespace Tetris.Game.Tests.Visual
{
    public class TestSceneMainScreen : TetrisTestScene
    {
        public TestSceneMainScreen()
        {
            Add(new ScreenStack(new MainScreen()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
