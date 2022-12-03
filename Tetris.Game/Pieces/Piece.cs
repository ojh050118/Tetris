using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics.Containers;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public abstract class Piece : Container<Block>
    {
        public abstract Block[,] Shape { get; }
        public abstract PieceType PieceType { get; }
        public Block[] Blocks => Array.FindAll(Shape.ToLinear(), b => b != null);

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

        [BackgroundDependencyLoader]
        private void load()
        {
            ArrangeBlockPosition();
            Size = ComputePieceAreaSize();
            Children = Array.FindAll(Shape.ToLinear(), b => b != null);
        }

        protected void ArrangeBlockPosition()
        {
            Vector2 offset = Vector2.Zero;

            for (int i = 0; i < Shape.Rank; i++)
            {
                for (int j = 0; j < Shape.GetLength(i); j++)
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
