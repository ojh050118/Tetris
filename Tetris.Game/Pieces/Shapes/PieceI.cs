using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceI : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                null, null, null, null
            },
            {
                new Block(), new Block(), new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.I;

        public PieceI()
        {
            PieceColour = new Color4(52, 179, 132, 255);
        }
    }
}
