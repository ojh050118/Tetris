using osu.Framework.Allocation;
using osu.Framework.Graphics;
using Tetris.Game.Graphics.Backgrounds;

namespace Tetris.Game.Screens.Play
{
    public class Player : TetrisScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new LiveBackground
                {
                    RelativeSizeAxes = Axes.Both
                },
                new Stage
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            };
        }
    }
}
