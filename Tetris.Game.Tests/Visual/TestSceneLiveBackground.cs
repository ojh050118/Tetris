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
            AddSliderStep("Speed rate", 0.1f, 5f, 1f, r => bg.SpeedRate = r);
            AddSliderStep("Dim", 0f, 1f, 0f, a => bg.Dim = a);
            AddStep("Reset", bg.Reset);
        }
    }
}


