using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public class Block : Container, IEquatable<Block>
    {
        public const int SIZE = 30;

        public Quad Quad;

        public Color4 BlockColour
        {
            get => blockColour;
            set
            {
                if (blockColour.Equals(value) || Count == 0)
                    return;

                blockColour = value;
                InternalChild.Colour = value;
            }
        }

        public Block()
        {
            Size = new Vector2(SIZE);
            InternalChild = new Box
            {
                Colour = blockColour,
                RelativeSizeAxes = Axes.Both
            };
        }

        private Color4 blockColour;

        public bool Equals(Block block)
        {
            return Position.Equals(block.Position) &&
                   BlockColour.Equals(block.BlockColour);
        }
    }
}
