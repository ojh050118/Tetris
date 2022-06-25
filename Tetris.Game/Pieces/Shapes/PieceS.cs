using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceS : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, true, true, false },
            new[] { true, true, false, false }
        };

        public override PieceType PieceType => PieceType.S;

        public override Color4 PieceColour => new Color4(144, 192, 65, 255);
    }
}
