using osu.Framework.Graphics;
using Tetris.Game.Screens.Play;

namespace Tetris.Game.Tests.Visual;

public class TestScenePiecceStage : TetrisTestScene
{
    public TestScenePiecceStage()
    {
        Add(new PieceStage
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
    }
}
