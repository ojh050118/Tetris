using osuTK;
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

        public override Vector2 RotateOriginPosition => new Vector2(30, 30);

        public PieceS()
        {
            PieceColour = new Color4(144, 192, 65, 255);
        }
    }
}
