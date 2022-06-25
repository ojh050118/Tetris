using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceL : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, false, true, false },
            new[] { true, true, true, false }
        };

        public override PieceShape PieceType => PieceShape.L;

        public override Color4 PieceColour => new Color4(201, 119, 70, 255);
    }
}
