using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceT : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                null, new Block(), null
            },
            {
                new Block(), new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.T;

        public PieceT()
        {
            PieceColour = new Color4(164, 70, 154, 255);
        }
    }
}
