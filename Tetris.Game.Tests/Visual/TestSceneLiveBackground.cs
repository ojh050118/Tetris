using osu.Framework.Graphics;
using Tetris.Game.Graphics.Backgrounds;

namespace Tetris.Game.Tests.Visual
{
    public class TestSceneLiveBackground : TetrisTestScene
    {
        public TestSceneLiveBackground()
        {
            LiveBackground bg;

            Add(bg = new LiveBackground { RelativeSizeAxes = Axes.Both });
            AddStep("Reset", bg.Reset);
        }
    }
}


