using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using Tetris.Game.Input;
using Tetris.Resources;

namespace Tetris.Game
{
    public class TetrisGameBase : osu.Framework.Game
    {
        protected override Container<Drawable> Content { get; }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        protected TetrisGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer());
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var largeStore = new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, @"Textures")));

            Resources.AddStore(new DllResourceStore(typeof(TetrisResources).Assembly));

            dependencies.CacheAs(largeStore);
            dependencies.CacheAs(this);

            base.Content.Add(new TetrisKeyBindingContainer(this));
        }
    }
}
