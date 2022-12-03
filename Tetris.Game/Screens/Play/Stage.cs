using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using Tetris.Game.Pieces;

namespace Tetris.Game.Screens.Play
{
    public class Stage : Container
    {
        public const int STAGE_WIDTH = 300;
        public const int STAGE_HEIGHT = 600;

        private PieceStage stage;

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;
            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Size = new Vector2(STAGE_WIDTH, STAGE_HEIGHT),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Color4.Black,
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.9f
                        },
                        new RectangularGridLine(new Vector2(STAGE_WIDTH, STAGE_HEIGHT))
                        {
                            RelativeSizeAxes = Axes.Both,
                            Spacing = new Vector2(Block.SIZE)
                        },
                        stage = new PieceStage()
                    }
                },
            };
        }
    }
}
