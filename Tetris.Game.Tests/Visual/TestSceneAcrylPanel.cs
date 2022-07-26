using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using osuTK.Graphics;
using Tetris.Game.Screens;

namespace Tetris.Game.Tests.Visual
{
    public class TestSceneAcrylPanel : TetrisTestScene
    {
        public TestSceneAcrylPanel()
        {
            BufferedContainer container;

            Add(container = new BufferedContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new TestSceneLiveBackground()
            });
            Add(new AcrylPanel(container.CreateView()));
        }
    }
}
