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

        private Box backgroundBox;
        private Container background;
        private Container movingBackground;
        private Box dim;

        private float speedRate = 1;

        public float SpeedRate
        {
            get => speedRate;
            set
            {
                if (value <= 0 || float.IsInfinity(value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                speedRate = value;
            }
        }

        public float Dim
        {
            get => dim.Alpha;
            set => dim.Alpha = value;
        }

        public bool CreateNewBox { get; private set; } = false;

        public const int MAX_BOXES = 12;

        public LiveBackground()
        {
            RelativeSizeAxes = Axes.Both;
            dim = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                Alpha = 0
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                backgroundBox = new Box
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
                },
                dim
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            start();
        }

        protected override void Update()
        {
            base.Update();

            // 우측 상단을 넘어 화면에 보이지 않을때 삭제합니다.
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
                var size = new Vector2(RNG.NextSingle(100, 400), RNG.Next(4, 7));
                var box = createSmallBox(size, currentColor.Multiply(RNG.Next(1, 10)).Lighten(0.5f));
                box.X = RNG.NextSingle(-DrawWidth, DrawWidth);

                movingBackground.Add(box);
                box.MoveToOffset(new Vector2(DrawWidth, -DrawWidth) * 2, 1 / speedRate * box.Width * 0.01f * 20000).Expire();
            }
        }

        private void start()
        {
            if (background.Count != 0 && CreateNewBox)
                return;

            background.AddRange(createBackground(4));
            CreateNewBox = true;
        }

        public void Reset()
        {
            CreateNewBox = false;

            background.Clear();
            movingBackground.Clear();

            currentColor = createRandomColor().Darken(6);
            backgroundBox.Colour = currentColor;

            background.AddRange(createBackground(4));
            CreateNewBox = true;
        }

        private Container[] createBackground(int boxAmount)
        {
            Container[] bg = new Container[boxAmount];

            for (int i = 0; i < boxAmount; i++)
            {
                var newColor = currentColor.Multiply(RNG.NextSingle(3)).Lighten(0.3f);
                var box = createBigBox(newColor);
                box.X = RNG.NextSingle(-DrawWidth + DrawHeight, DrawWidth) / 2;
                bg[i] = box;
            }

            return bg;
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
