using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;
using Tetris.Game.Graphics;
using Tetris.Game.Graphics.Backgrounds;
using Tetris.Game.Graphics.Sprites;
using Tetris.Game.Screens.Play;

namespace Tetris.Game.Screens
{
    public class MainScreen : TetrisScreen
    {
        private BufferedContainer background;

        public override bool BlockExit => true;

        [BackgroundDependencyLoader]
        private void load(TetrisGameBase game)
        {
            InternalChildren = new Drawable[]
            {
                background = new BufferedContainer
                {
                    RedrawOnScale = false,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new LiveBackground
                        {
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                },
                new AcrylPanel(background.CreateView())
                {
                    Content = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(0, 10),
                        Children = new Drawable[]
                        {
                            new GlowingSpriteText
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Text = "Tetris",
                                GlowColour = Color4.DeepSkyBlue,
                                Font = TetrisFont.Default.With(size: 36, weight: FontWeight.Bold),
                            },
                            new TextButton("Play")
                            {
                                Action = () => this.Push(new Player())
                            },
                            new TextButton("Settings"),
                            new TextButton("Exit")
                            {
                                Action = game.RequestExit
                            }
                        }
                    }
                }
            };
        }

        private class TextButton : ClickableContainer
        {
            private readonly string text;
            private Box hover;
            private SpriteText spriteText;

            private const int wind_effect_amount = 5;

            private const float font_size = 32;

            private float textSize
            {
                get => spriteText.Font.Size;
                set => spriteText.Font = TetrisFont.Default.With(size: value);
            }

            public TextButton(string text)
            {
                this.text = text;
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
                AutoSizeDuration = 1000;
                AutoSizeEasing = Easing.OutPow10;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Children = new Drawable[]
                {
                    spriteText = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = text,
                        Font = TetrisFont.Default.With(size: font_size),
                        Margin = new MarginPadding(4),
                        Shadow = true,
                        ShadowColour = Color4.Black.Opacity(0.5f)
                    },
                    hover = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Width = 0.8f,
                        Colour = ColourInfo.GradientHorizontal(Color4.White.Opacity(0.4f), Color4.Transparent),
                        Alpha = 0
                    }
                };
                Action += createClickEffect;
            }

            protected override bool OnHover(HoverEvent e)
            {
                hover.FadeIn(100, Easing.OutPow10);
                createWindEffect();
                this.TransformTo("textSize", 40f, 1000, Easing.OutPow10);

                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                base.OnHoverLost(e);

                this.TransformTo("textSize", 32f, 1000, Easing.OutPow10);

                foreach (var d in Array.FindAll(Children.ToArray(), h => h.GetType() != typeof(SpriteText)))
                    d.FadeOut(700, Easing.Out);
            }

            private void createWindEffect()
            {
                for (int count = 0; count < wind_effect_amount; count++)
                {
                    var wind = createWind(RNG.NextSingle(0.5f));

                    Add(wind);
                    wind.FadeOut().MoveToY(RNG.NextSingle(-font_size / 2, font_size / 2)).FadeIn().MoveToX(RNG.NextSingle(200, 450), RNG.NextSingle(1000, 1500), Easing.OutPow10).FadeOut(RNG.NextSingle(2000, 3000), Easing.OutPow10).Expire();
                }
            }

            private void createClickEffect()
            {
                createWindEffect();
                var wind = createWind(height: 1);
                wind.RelativeSizeAxes = Axes.Both;
                Add(wind);
                wind.MoveToX(DrawWidth, 1000, Easing.OutPow10).FadeOut(1500, Easing.OutPow10).Expire();
            }

            private Drawable createWind(float width = 0.5f, float height = 2)
            {
                return new GridContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.X,
                    Width = width,
                    Height = height,
                    Depth = 1,
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = ColourInfo.GradientHorizontal(Color4.Transparent, Color4.White)
                            },
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = ColourInfo.GradientHorizontal(Color4.White, Color4.Transparent)
                            }
                        }
                    }
                };
            }
        }
    }
}
