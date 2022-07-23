using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Screens
{
    public class MainScreen : Screen
    {
        private BufferedContainer background;

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures, TetrisGameBase game)
        {
            InternalChildren = new Drawable[]
            {
                background = new BufferedContainer
                {
                    RedrawOnScale = false,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Sprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Texture = textures.Get("bg1.jfif"),
                            FillMode = FillMode.Fill
                        }
                    }
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.5f,
                    Masking = true,
                    Shear = new Vector2(0.2f, 0),
                    Children = new Drawable[]
                    {
                        new BufferedContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Masking = true,
                            BlurSigma = new Vector2(7),
                            Children = new Drawable[]
                            {
                                background.CreateView().With(d =>
                                {
                                    d.RelativeSizeAxes = Axes.Both;
                                    d.SynchronisedDrawQuad = true;
                                    d.DisplayOriginalEffects = true;
                                })
                            }
                        },
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0, 10),
                            Children = new Drawable[]
                            {
                                new TextButton("Play"),
                                new TextButton("Settings"),
                                new TextButton("Exit")
                                {
                                    Action = game.RequestExit
                                }
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

            private const int wind_effect_amount = 5;

            public TextButton(string text)
            {
                this.text = text;
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Children = new Drawable[]
                {
                    new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = text,
                        Font = FontUsage.Default.With(size: 28),
                        Margin = new MarginPadding(4),
                        Shadow = true,
                        ShadowColour = Color4.Black.Opacity(0.5f)
                    },
                    hover = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Width = 0.8f,
                        Colour = ColourInfo.GradientHorizontal(Color4.White.Opacity(0.4f), Color4.Transparent)
                    }
                };
            }

            protected override bool OnHover(HoverEvent e)
            {
                hover.FadeIn(100, Easing.OutPow10);

                for (int count = 0; count < wind_effect_amount; count++)
                {
                    var wind = createWind(RNG.NextSingle(0.5f));

                    Add(wind);
                    wind.FadeOut().MoveToY(RNG.NextSingle(-14, 14)).FadeIn().MoveToX(RNG.NextSingle(200, 450), RNG.NextSingle(1000, 1500), Easing.OutPow10).FadeOut(RNG.NextSingle(2000, 3000), Easing.OutPow10).Expire();
                }

                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                base.OnHoverLost(e);

                foreach (var d in Array.FindAll(Children.ToArray(), h => h.GetType() != typeof(SpriteText)))
                    d.FadeOut(700, Easing.Out);
            }

            protected override bool OnClick(ClickEvent e)
            {
                Box box;

                Add(box = new Box
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.8f,
                    Colour = ColourInfo.GradientHorizontal(Color4.Transparent, Color4.White.Opacity(0.4f))
                });
                box.FadeOut(1000, Easing.OutPow10).Expire();
                hover.FlashColour(Color4.Transparent, 1000, Easing.OutPow10);

                return base.OnClick(e);
            }

            private Drawable createWind(float width = 0.5f)
            {
                return new GridContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Width = width,
                    Depth = 1,
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize)
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.X,
                                Height = 2,
                                Colour = ColourInfo.GradientHorizontal(Color4.Transparent, Color4.White)
                            },
                            new Box
                            {
                                RelativeSizeAxes = Axes.X,
                                Height = 2,
                            },
                            new Box
                            {
                                RelativeSizeAxes = Axes.X,
                                Height = 2,
                                Colour = ColourInfo.GradientHorizontal(Color4.White, Color4.Transparent)
                            }
                        }
                    }
                };
            }
        }
    }
}
