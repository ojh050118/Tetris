using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceO : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                new Block(), new Block()
            },
            {
                new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.O;

        public override Vector2 RotateOriginPosition => new Vector2(15, 15);

        public PieceO()
        {
            PieceColour = new Color4(196, 172, 69, 255);
        }
    }
}
