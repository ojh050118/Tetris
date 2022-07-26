using osu.Framework.Graphics;
using Tetris.Game.Screens.Play;

namespace Tetris.Game.Tests.Visual
{
    public class TestSceneStage : TetrisTestScene
    {
        public TestSceneStage()
        {
            Add(new Stage
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}
