using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;

namespace Tetris.Game.Input
{
    public class TetrisKeyBindingContainer : KeyBindingContainer<InputAction>
    {
        private readonly Drawable handler;

        [CanBeNull]
        private InputManager parentInputManager;

        public override IEnumerable<IKeyBinding> DefaultKeyBindings => GlobalKeyBindings;

        public IEnumerable<KeyBinding> GlobalKeyBindings => new[]
        {
            new KeyBinding(InputKey.Down, InputAction.SoftDrop),
            new KeyBinding(InputKey.Space, InputAction.HardDrop),
            new KeyBinding(InputKey.Left, InputAction.PieceLeft),
            new KeyBinding(InputKey.Right, InputAction.PieceRight),
            new KeyBinding(InputKey.Up, InputAction.RotateClockwise),
            new KeyBinding(InputKey.Z, InputAction.RotateCounterclockwise),
            new KeyBinding(InputKey.X, InputAction.RotateClockwise),
            new KeyBinding(InputKey.A, InputAction.Rotate180),
            new KeyBinding(InputKey.Shift, InputAction.SwapHoldPiece),
            new KeyBinding(InputKey.C, InputAction.SwapHoldPiece),
            new KeyBinding(InputKey.R, InputAction.Retry),
            new KeyBinding(InputKey.Escape, InputAction.Exit)
        };

        public TetrisKeyBindingContainer(TetrisGameBase game)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<InputAction>)
                handler = game;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            parentInputManager = GetContainingInputManager();
        }

        protected override IEnumerable<Drawable> KeyBindingInputQueue
        {
            get
            {
                var inputQueue = parentInputManager?.NonPositionalInputQueue ?? base.KeyBindingInputQueue;

                return handler != null ? inputQueue.Prepend(handler) : inputQueue;
            }
        }
    }
}
