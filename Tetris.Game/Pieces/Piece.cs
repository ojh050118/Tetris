using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public abstract class Piece : Container
    {
        public abstract Block[,] Shape { get; }
        public abstract PieceType PieceType { get; }
        public Block[] Blocks => Array.FindAll(Shape.ToLinear(), b => b != null);

        public abstract Vector2 RotateOriginPosition { get; }

        public Color4 PieceColour
        {
            get => pieceColour;
            set
            {
                if (pieceColour.Equals(value))
                    return;

                pieceColour = value;
                Blocks.ForEach(b => b.BlockColour = value);
            }
        }

        private Color4 pieceColour;
        private float rotation;

        [BackgroundDependencyLoader]
        private void load()
        {
            ArrangeBlockPosition();
            Size = ComputePieceAreaSize();
            Children = Blocks;
        }

        public void Rotate(PieceRotateDirection direction, double duration = 0, Easing easing = Easing.None)
        {
            rotation = getSafeRotation(rotation + (int)direction);

            foreach (var block in Blocks)
            {
                float newRadian = MathHelper.DegreesToRadians((int)direction);
                var p2X = block.X - RotateOriginPosition.X;
                var p2Y = block.Y - RotateOriginPosition.Y;
                var destX = (float)(p2X * Math.Cos(newRadian) - p2Y * Math.Sin(newRadian));
                var destY = (float)(p2X * Math.Sin(newRadian) + p2Y * Math.Cos(newRadian));

                block.FinishTransforms();
                block.MoveTo(new Vector2(destX, destY) + RotateOriginPosition, duration, easing);
            }
        }

        public void Move(PieceMoveDirection direction, double duration = 0, Easing easing = Easing.None)
        {
            this.MoveToOffset(new Vector2((direction == PieceMoveDirection.Left ? -1 : 1) * Block.SIZE, 0), duration, easing);
        }

        private float getSafeRotation(float rotation)
        {
            while (rotation > 360)
                rotation -= 360;

            while (rotation < 0)
                rotation += 360;

            if (Precision.AlmostEquals(rotation, 360))
                rotation = 0;

            return rotation;
        }

        protected void ArrangeBlockPosition()
        {
            Vector2 offset = Vector2.Zero;

            for (int i = 0; i < Shape.Rank; i++)
            {
                for (int j = 0; j < Shape.GetLength(1); j++)
                {
                    if (Shape[i, j] != null)
                        Shape[i, j].Position = offset;

                    offset.X += Block.SIZE;
                }

                offset.X = 0;
                offset.Y += Block.SIZE;
            }
        }

        protected Vector2 ComputePieceAreaSize()
        {
            var rowLength = Shape.Rank;
            var columnLength = Shape.GetLength(1);

            return new Vector2(Math.Max(rowLength, columnLength) * Block.SIZE);
        }
    }
}
