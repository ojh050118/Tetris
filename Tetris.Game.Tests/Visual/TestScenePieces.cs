using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Pieces;
using Tetris.Game.Pieces.Shapes;

namespace Tetris.Game.Tests.Visual
{
    public class TestScenePieces : TetrisTestScene
    {
        public TestScenePieces()
        {
            Add(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Content = new[]
                {
                    new Drawable[]
                    {
                        new PieceI { Anchor = Anchor.Centre, Origin = Anchor.Centre },
                        new PieceJ { Anchor = Anchor.Centre, Origin = Anchor.Centre },
                        new PieceL { Anchor = Anchor.Centre, Origin = Anchor.Centre },
                        new PieceO { Anchor = Anchor.Centre, Origin = Anchor.Centre }
                    },
                    new Drawable[]
                    {
                        new PieceS { Anchor = Anchor.Centre, Origin = Anchor.Centre },
                        new PieceT { Anchor = Anchor.Centre, Origin = Anchor.Centre },
                        new PieceZ { Anchor = Anchor.Centre, Origin = Anchor.Centre },
                        new Block { Anchor = Anchor.Centre, Origin = Anchor.Centre }
                    }
                }
            });
        }
    }
}
