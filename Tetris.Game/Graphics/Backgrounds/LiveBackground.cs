using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Graphics.Backgrounds
{
    public class LiveBackground : Container
    {
        private Func<Color4> createRandomColor => () => new Color4(RNG.NextSingle(0.2f, 1), RNG.NextSingle(0.2f, 1), RNG.NextSingle(0.2f, 1), 255);

        private Color4 currentColor;

        private Container background;
        private Container movingBackground;

        public bool CreateNewBox = true;

        public const int MAX_BOXES = 12;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = currentColor = createRandomColor().Darken(6)
                },
                background = new Container
                {
                    Name = "Background box",
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fill
                },
                movingBackground = new Container
                {
                    Name = "Moving box",
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.Both,
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            for (int count = 0; count < 4; count++)
            {
                var newColor = currentColor.Multiply(RNG.NextSingle(3)).Lighten(0.3f);
                var box = createBigBox(newColor);
                box.X = RNG.NextSingle(-DrawWidth + DrawHeight, DrawWidth) / 2;
                background.Add(box);
            }
        }

        protected override void Update()
        {
            base.Update();

            foreach (var d in movingBackground.Children)
            {
                if (-d.Y - d.Width * Math.Sin(Math.PI / 4) > DrawHeight || d.X - d.Width * Math.Cos(Math.PI / 4) > DrawWidth)
                {
                    d.FinishTransforms();
                    d.Expire();
                }
            }

            if (!CreateNewBox)
                return;

            if (movingBackground.Count < 12)
            {
                var size = new Vector2(RNG.NextSingle(100, 400), RNG.Next(4, 6));
                var box = createSmallBox(size, currentColor.Multiply(RNG.Next(1, 10)).Lighten(0.5f));
                box.X = RNG.NextSingle(-DrawWidth, DrawWidth);

                movingBackground.Add(box);
                box.MoveToOffset(new Vector2(DrawWidth, -DrawWidth), box.Width * 0.01f * 10000).Expire();
            }
        }

        private Container createSmallBox(Vector2 size, Color4 color)
        {
            return new Container
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.CentreRight,
                Masking = true,
                Rotation = -45,
                Size = size,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = color,
                },
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Colour = color.Opacity(0.5f),
                    Radius = RNG.Next(5, 20)
                }
            };
        }

        private Container createBigBox(Color4 color)
        {
            return new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                Shear = new Vector2(1, 0),
                Width = RNG.NextSingle(0.2f, 0.5f),
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = color,
                },
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Colour = Color4.Black,
                    Radius = 100,
                    Roundness = 10,
                }
            };
        }
    }
}
