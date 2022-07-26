using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Input;

namespace Tetris.Game.Screens
{
    public class TetrisScreen : Screen, IKeyBindingHandler<InputAction>
    {
        public virtual bool BlockExit => false;

        public TetrisScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            base.OnEntering(e);

            CornerRadius = 20;
            this.MoveToX(DrawWidth).MoveToX(0, 1000, Easing.OutPow10);
            this.ScaleTo(0.95f).ScaleTo(1, 1000, Easing.OutPow10);
            this.TransformTo("CornerRadius", 0f, 1000, Easing.OutPow10);
        }

        public override bool OnExiting(ScreenExitEvent e)
        {
            this.MoveToX(DrawWidth, 1000, Easing.OutPow10);
            this.TransformTo("CornerRadius", 20f, 1000, Easing.OutPow10);

            return base.OnExiting(e);
        }

        public override void OnResuming(ScreenTransitionEvent e)
        {
            base.OnResuming(e);

            CornerRadius = 20;
            this.ScaleTo(1f, 1000, Easing.OutPow10);
            this.TransformTo("CornerRadius", 0f, 1000, Easing.OutPow10);
        }

        public override void OnSuspending(ScreenTransitionEvent e)
        {
            base.OnSuspending(e);

            this.ScaleTo(0.95f, 1000, Easing.OutPow10);
            this.TransformTo("CornerRadius", 20f, 1000, Easing.OutPow10);
        }

        public virtual bool OnPressed(KeyBindingPressEvent<InputAction> e)
        {
            switch (e.Action)
            {
                case InputAction.Exit:
                    if (!BlockExit)
                        OnExit();
                    return true;
            }

            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<InputAction> e)
        {
        }

        public virtual void OnExit() => this.Exit();
    }
}
