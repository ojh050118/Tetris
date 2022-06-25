using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class PieceT : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, true, false, false },
            new[] { true, true, true, false }
        };

        public override PieceType PieceType => PieceType.T;

        public override Color4 PieceColour => new Color4(164, 70, 154, 255);
    }
}
