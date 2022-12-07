using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceL : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                null, null, new Block()
            },
            {
                new Block(), new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.L;

        public override Vector2 RotateOriginPosition => new Vector2(30, 30);

        public PieceL()
        {
            PieceColour = new Color4(201, 119, 70, 255);
        }
    }
}
