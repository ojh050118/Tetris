using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public abstract class Piece : Container, IHasPieceProperty
    {
        public const int SIZE = 30;

        public abstract Color4 PieceColour { get; }

        private bool[][] shape;

        public virtual bool[][] Shape
        {
            get => shape;
            set
            {
                if (value.Length != 2)
                    throw new ArgumentException("행의 길이는 2여야합니다.");

                for (int index = 0; index < value.Length; index++)
                {
                    if (value[index].Length != 4)
                        throw new ArgumentException($"[{index}]의 길이는 4이여야 합니다.");
                }

                shape = value;
            }
        }

        public Vector2 InitialPosition { get; set; }

        public abstract PieceShape PieceType { get; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Size = new Vector2(SIZE);
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = PieceColour
            };
        }
    }
}
