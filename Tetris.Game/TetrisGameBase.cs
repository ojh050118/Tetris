using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using Tetris.Game.Input;
using Tetris.Resources;

namespace Tetris.Game
{
    public class TetrisGameBase : osu.Framework.Game
    {
        protected override Container<Drawable> Content { get; }

        protected TetrisGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer());
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(TetrisResources).Assembly));
            base.Content.Add(new TetrisKeyBindingContainer(this));
        }
    }
}
