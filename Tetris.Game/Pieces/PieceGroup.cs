using System.Linq;
using osuTK;

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

        private Vector2 position;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public float X
        {
            get => position.X;
            set => position.X = value;
        }

        public float Y
        {
            get => position.Y;
            set => position.Y = value;
        }

        public PieceGroup(Piece[] pieces)
        {
            Pieces = pieces;
            PieceType = pieces.First().PieceType;
            Shape = pieces.First().Shape;
        }
    }
}
