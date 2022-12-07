using osuTK;
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

        public override Vector2 RotateOriginPosition => new Vector2(30, 30);

        public PieceT()
        {
            PieceColour = new Color4(164, 70, 154, 255);
        }
    }
}
