using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osuTK;
using Tetris.Game.Input;
using Tetris.Game.Pieces;
using Tetris.Game.Pieces.Shapes;

namespace Tetris.Game.Screens.Play
{
    public class PieceStage : Container<Piece>, IKeyBindingHandler<InputAction>
    {
        private Piece piece;

        public PieceStage()
        {
            Size = new Vector2(Block.SIZE * 10, Block.SIZE * 20);
            Add(piece = new PieceI() { Anchor = Anchor.TopCentre, Origin = Anchor.BottomCentre });

            if (typeof(PieceI) == piece.GetType())
                piece.Y += Block.SIZE;
        }

        public virtual bool OnPressed(KeyBindingPressEvent<InputAction> e)
        {
            switch (e.Action)
            {
                case InputAction.RotateClockwise:
                    piece.Rotate(PieceRotateDirection.Clockwise);
                    break;

                case InputAction.RotateCounterclockwise:
                    piece.Rotate(PieceRotateDirection.CounterClockwise);
                    break;

                case InputAction.Rotate180:
                    piece.Rotate(PieceRotateDirection.Rotate180);
                    break;

                case InputAction.PieceLeft:
                    piece.Move(PieceMoveDirection.Left);
                    break;

                case InputAction.PieceRight:
                    piece.Move(PieceMoveDirection.Right);
                    break;

                case InputAction.SoftDrop:
                    break;

                case InputAction.HardDrop:
                    break;

                default:
                    return false;
            }

            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<InputAction> e)
        {
        }
    }
}
