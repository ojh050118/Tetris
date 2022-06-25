using System;
using System.Linq;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Utils;
using osuTK;

namespace Tetris.Game.Pieces.Group
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

        public PieceType PieceType { get; }

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

        public int ID { get; }

        public PieceGroup(int id, Piece[] pieces)
        {
            ID = id;
            Pieces = pieces;
            PieceType = pieces.First().PieceType;
            Shape = pieces.First().Shape;

            foreach (var piece in pieces)
                piece.Group = this;
        }

        public void SetDefaultPiecePosition(Vector2 newPosition)
        {
            foreach (var piece in Pieces)
            {
                piece.Position = piece.InitialPosition + newPosition;
                // 완전히 겹치는지 검사하기 위해 1픽셀을 줄입니다.
                piece.Quad = new Quad(piece.Position.X, piece.Position.Y, piece.Width - 1, piece.Height - 1);
            }

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
            return group != null &&
                   ID == group.ID &&
                   RotateCount == group.RotateCount &&
                   Rotation == group.Rotation &&
                   PieceType == group.PieceType &&
                   Precision.AlmostEquals(Position, group.Position) &&
                   Precision.AlmostEquals(Size, group.Size);
        }

        public override string ToString()
        {
            return $"ID: {ID} | PieceType: {PieceType} | Position: {Position} | Size: {Size}";
        }
    }
}
