using osu.Framework.Graphics;
using Tetris.Game.Graphics.Backgrounds;

namespace Tetris.Game.Tests.Visual
{
    public class TestSceneLiveBackground : TetrisTestScene
    {
        public TestSceneLiveBackground()
        {
            Add(new LiveBackground { RelativeSizeAxes = Axes.Both });
        }
    }
}


