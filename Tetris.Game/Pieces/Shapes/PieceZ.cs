using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceZ : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { true, true, false, false },
            new[] { false, true, true, false }
        };

        public override PieceType PieceType => PieceType.Z;

        public override Color4 PieceColour => new Color4(193, 62, 69, 255);
    }
}
