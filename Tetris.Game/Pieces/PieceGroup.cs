using System;
using System.Linq;
using osu.Framework.Utils;
using osuTK;

namespace Tetris.Game.Pieces
{
    public class PieceGroup : IEquatable<PieceGroup>
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

        public Vector2 Size => new Vector2(getMaxX() - getMinX() + Piece.SIZE, getMaxY() - getMinY() + Piece.SIZE);

        public float Width => getMaxX() - getMinX() + Piece.SIZE;

        public float Height => getMaxY() - getMinY() + Piece.SIZE;

        public PieceGroup(Piece[] pieces)
        {
            Pieces = pieces;
            PieceType = pieces.First().PieceType;
            Shape = pieces.First().Shape;

            foreach (var piece in pieces)
                piece.Group = this;
        }

        public void SetDefaultPiecePosition(Vector2 newPosition)
        {
            foreach (var piece in Pieces)
                piece.Position = piece.InitialPosition + newPosition;

            Position = newPosition;
        }

        private float getMinX()
        {
            float x = float.MaxValue;

            for (int i = 0; i < Pieces.Length; i++)
            {
                if (Pieces[i].X < x)
                    x = Pieces[i].X;
            }

            return x;
        }

        private float getMaxX()
        {
            float x = float.MinValue;

            for (int i = 0; i < Pieces.Length; i++)
            {
                if (Pieces[i].X > x)
                    x = Pieces[i].X;
            }

            return x;
        }

        private float getMinY()
        {
            float y = float.MaxValue;

            for (int i = 0; i < Pieces.Length; i++)
            {
                if (Pieces[i].Y < y)
                    y = Pieces[i].Y;
            }

            return y;
        }

        private float getMaxY()
        {
            float y = float.MinValue;

            for (int i = 0; i < Pieces.Length; i++)
            {
                if (Pieces[i].Y > y)
                    y = Pieces[i].Y;
            }

            return y;
        }

        public bool Equals(PieceGroup group)
        {
            return RotateCount == group.RotateCount &&
                   Rotation == group.Rotation &&
                   PieceType == group.PieceType &&
                   Shape.SequenceEqual(group.Shape) &&
                   Precision.AlmostEquals(Position, group.Position) &&
                   Precision.AlmostEquals(Size, group.Size);
        }


        public override string ToString()
        {
            return $"PieceType: {PieceType} | Position: {Position} | Size: {Size}";
        }
    }
}
