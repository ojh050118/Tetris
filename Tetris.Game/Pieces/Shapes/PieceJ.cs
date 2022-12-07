using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceJ : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                new Block(), null, null
            },
            {
                new Block(), new Block(), new Block()
            }
        };

        public override PieceType PieceType => PieceType.J;

        public override Vector2 RotateOriginPosition => new Vector2(30, 30);

        public PieceJ()
        {
            PieceColour = new Color4(97, 79, 182, 255);
        }
    }
}
