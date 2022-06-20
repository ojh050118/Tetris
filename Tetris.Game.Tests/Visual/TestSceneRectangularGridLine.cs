using osu.Framework.Graphics;
using osuTK;

namespace Tetris.Game.Tests.Visual;

public class TestSceneRectangularGridLine : TetrisTestScene
{
    public TestSceneRectangularGridLine()
    {
        Add(new RectangularGridLine(DrawSize / 2)
        {
            RelativeSizeAxes = Axes.Both,
            Spacing = new Vector2(30)
        });
    }
}
