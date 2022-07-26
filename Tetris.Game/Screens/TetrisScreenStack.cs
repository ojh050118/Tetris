using osu.Framework.Screens;
using Tetris.Game.Graphics.Backgrounds;

namespace Tetris.Game.Screens
{
    public class TetrisScreenStack : ScreenStack
    {
        public int Count { get; private set; }

        public TetrisScreenStack()
        {
            InternalChild = new LiveBackground
            {
                Dim = 0.8f
            };

            ScreenPushed += (prev, next) => Count++;
            ScreenExited += (prev, next) => Count--;
        }
    }
}
