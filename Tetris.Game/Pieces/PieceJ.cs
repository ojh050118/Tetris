using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceJ : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { true, false, false, false },
            new[] { true, true, true, false }
        };

        public override PieceShape PieceType => PieceShape.J;

        public override Color4 PieceColour => new Color4(97, 79, 182, 255);
    }
}
