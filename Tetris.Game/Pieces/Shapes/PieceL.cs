using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceL : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                null, null, null, new Block()
            },
            {
                null, new Block(), new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.L;

        public PieceL()
        {
            PieceColour = new Color4(201, 119, 70, 255);
        }
    }
}
