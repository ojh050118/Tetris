using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Pieces;
using Tetris.Game.Pieces.Shapes;

namespace Tetris.Game.Screens.Play
{
    public class PieceStage : Container<Piece>
    {
        public PieceStage()
        {
            AutoSizeAxes = Axes.Both;
            Add(new PieceI());
        }
    }
}
