using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceL : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, false, true, false },
            new[] { true, true, true, false }
        };

        public override PieceType PieceType => PieceType.L;

        public override Color4 PieceColour => new Color4(201, 119, 70, 255);
    }
}
