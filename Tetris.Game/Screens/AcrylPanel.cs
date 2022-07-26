using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Tetris.Game.Screens
{
    public class AcrylPanel : Container
    {
        private readonly BufferedContainer view;

        public new Container<Drawable> Content;

        public AcrylPanel(BufferedContainerView<Drawable> view)
        {
            RelativeSizeAxes = Axes.Both;
            Width = 0.5f;
            Masking = true;
            Content = new Container { RelativeSizeAxes = Axes.Both };
            this.view = new BufferedContainer
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                BlurSigma = new Vector2(7),
                Child = view.With(d =>
                {
                    d.RelativeSizeAxes = Axes.Both;
                    d.SynchronisedDrawQuad = true;
                    d.DisplayOriginalEffects = true;
                })
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Shear = new Vector2(0.2f, 0),
                    Masking = true,
                    Children = new Drawable[]
                    {
                        view,
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black,
                            Alpha = 0.2f
                        },
                        Content
                    }
                }
            };
        }
    }
}
