using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceZ : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                new Block(), new Block(), null
            },
            {
                null, new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.Z;

        public override Vector2 RotateOriginPosition => new Vector2(30, 30);

        public PieceZ()
        {
            PieceColour = new Color4(193, 62, 69, 255);
        }
    }
}
