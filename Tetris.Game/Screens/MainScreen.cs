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
                Action += createClickEffect;
            }

            protected override bool OnHover(HoverEvent e)
            {
                hover.FadeIn(100, Easing.OutPow10);
                createWindEffect();

                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                base.OnHoverLost(e);

                foreach (var d in Array.FindAll(Children.ToArray(), h => h.GetType() != typeof(SpriteText)))
                    d.FadeOut(700, Easing.Out);
            }

            private void createWindEffect()
            {
                for (int count = 0; count < wind_effect_amount; count++)
                {
                    var wind = createWind(RNG.NextSingle(0.5f));

                    Add(wind);
                    wind.FadeOut().MoveToY(RNG.NextSingle(-14, 14)).FadeIn().MoveToX(RNG.NextSingle(200, 450), RNG.NextSingle(1000, 1500), Easing.OutPow10).FadeOut(RNG.NextSingle(2000, 3000), Easing.OutPow10).Expire();
                }
            }

            private void createClickEffect()
            {
                Drawable wind;

                createWindEffect();
                Add(wind = createWind(height: 36));
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
