using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceO : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, true, true, false },
            new[] { false, true, true, false }
        };

        public override PieceShape PieceType => PieceShape.O;

        public override Color4 PieceColour => new Color4(196, 172, 69, 255);
    }
}
