using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceJ : Piece
    {
        public override Block[,] Shape { get; } =
        {
            {
                new Block(), null, null, null
            },
            {
                new Block(), new Block(), new Block(), null
            }
        };

        public override PieceType PieceType => PieceType.J;

        public PieceJ()
        {
            PieceColour = new Color4(97, 79, 182, 255);
        }
    }
}
