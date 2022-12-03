using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceS : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                null, new Block(), new Block()
            },
            {
                new Block(), new Block(), null
            }
        };

        public override PieceType PieceType => PieceType.S;

        public PieceS()
        {
            PieceColour = new Color4(144, 192, 65, 255);
        }
    }
}
