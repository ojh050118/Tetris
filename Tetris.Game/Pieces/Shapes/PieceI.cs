using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceI : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, false, false, false },
            new[] { true, true, true, true }
        };

        public override PieceShape PieceType => PieceShape.I;

        public override Color4 PieceColour => new Color4(52, 179, 132, 255);
    }
}
