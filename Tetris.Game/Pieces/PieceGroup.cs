using System.Linq;

namespace Tetris.Game.Pieces
{
    public class PieceGroup
    {
        public Piece[] Pieces { get; }

        public int RotateCount { get; set; }

        private int rotation;

        public int Rotation
        {
            get => rotation;
            set
            {
                rotation = value;

                while (rotation > 360)
                    rotation -= 360;

                while (rotation < 0)
                    rotation += 360;
            }
        }

        public PieceShape PieceType { get; }

        public bool[][] Shape { get; }

        public PieceGroup(Piece[] pieces)
        {
            Pieces = pieces;
            PieceType = pieces.First().PieceType;
            Shape = pieces.First().Shape;
        }
    }
}
