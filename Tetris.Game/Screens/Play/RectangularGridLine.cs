using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Layout;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Screens.Play
{
    public class RectangularGridLine : CompositeDrawable
    {
        public Vector2 StartPosition { get; }

        private Vector2 spacing = Vector2.One;

        public Vector2 Spacing
        {
            get => spacing;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                    throw new ArgumentException("그리드 간격은 양수여야 합니다.");

                spacing = value;
            }
        }

        private readonly LayoutValue gridCache = new LayoutValue(Invalidation.RequiredParentSizeToFit);

        public RectangularGridLine(Vector2 startPosition)
        {
            StartPosition = startPosition;

            AddLayout(gridCache);
        }

        protected override void Update()
        {
            base.Update();

            if (!gridCache.IsValid)
            {
                ClearInternal();
                createContent();
                gridCache.Validate();
            }
        }

        private void createContent()
        {
            var drawSize = DrawSize;

            generateGridLines(Direction.Horizontal, StartPosition.Y, 0, -Spacing.Y);
            generateGridLines(Direction.Horizontal, StartPosition.Y, drawSize.Y, Spacing.Y);

            generateGridLines(Direction.Vertical, StartPosition.X, 0, -Spacing.X);
            generateGridLines(Direction.Vertical, StartPosition.X, drawSize.X, Spacing.X);
        }

        private void generateGridLines(Direction direction, float startPosition, float endPosition, float spacing)
        {
            int index = 0;
            float currentPosition = startPosition;
            float lineWidth = DrawWidth / ScreenSpaceDrawQuad.Width * 2;

            List<Box> lines = new List<Box>();

            while (Precision.AlmostBigger((endPosition - currentPosition) * Math.Sign(spacing), 0))
            {
                var gridLine = new Box
                {
                    Colour = Color4.White,
                    Alpha = 0.1f
                };

                if (direction == Direction.Horizontal)
                {
                    gridLine.Origin = Anchor.BottomLeft;
                    gridLine.RelativeSizeAxes = Axes.X;
                    gridLine.Height = lineWidth;
                    gridLine.Y = currentPosition;
                }
                else
                {
                    gridLine.Origin = Anchor.TopRight;
                    gridLine.RelativeSizeAxes = Axes.Y;
                    gridLine.Width = lineWidth;
                    gridLine.X = currentPosition;
                }

                lines.Add(gridLine);

                index++;
                currentPosition = lineWidth + index * spacing;
            }

            if (lines.Count == 0)
                return;

            AddRangeInternal(lines);
        }
    }
}
