using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;
using Tetris.Game.Input;
using Tetris.Game.Pieces;
using Tetris.Game.Pieces.Group;

namespace Tetris.Game.Play
{
    public class PieceStage : Container<Piece>, IKeyBindingHandler<InputAction>
    {
        private PieceGroup group;

        private readonly RandomPieceGenerator rpg;

        public PieceStage()
        {
            rpg = new RandomPieceGenerator();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            Size = new Vector2(Stage.STAGE_WIDTH, Stage.STAGE_HEIGHT);
            addPieceGroup(rpg.NextPiece());
        }

        public virtual bool OnPressed(KeyBindingPressEvent<InputAction> e)
        {
            Vector2 moveOffset = Vector2.Zero;
            RotationDirection direction = RotationDirection.Clockwise;

            switch (e.Action)
            {
                case InputAction.RotateClockwise:
                    group.Rotate(direction = RotationDirection.Clockwise);
                    break;

                case InputAction.RotateCounterclockwise:
                    group.Rotate(direction = RotationDirection.Counterclockwise);
                    break;

                case InputAction.Rotate180:
                    group.Rotate(direction = RotationDirection.Clockwise);
                    checkForCollision(moveOffset, direction);
                    group.Rotate(direction = RotationDirection.Clockwise);
                    break;

                case InputAction.PieceLeft:
                    group.MoveToOffset(moveOffset = new Vector2(-Piece.SIZE, 0));
                    break;

                case InputAction.PieceRight:
                    group.MoveToOffset(moveOffset = new Vector2(Piece.SIZE, 0));
                    break;

                case InputAction.SoftDrop:
                    group.MoveToOffset(moveOffset = new Vector2(0, Piece.SIZE));
                    break;

                case InputAction.HardDrop:
                    addPieceGroup(rpg.NextPiece());
                    break;

                default:
                    return false;
            }

            checkForCollision(moveOffset, direction);
            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<InputAction> e)
        {
        }

        private void addPieceGroup(PieceType pieceType)
        {
            AddRange((group = createPieceGroup(pieceType)).Pieces);
        }

        private PieceGroup createPieceGroup(PieceType pieceType) => PieceGroupExtension.CreatePieceGroup(pieceType, new Vector2(Stage.STAGE_WIDTH / 2 - Piece.SIZE * 2, -Piece.SIZE * 2));

        private bool checkForCollision(Vector2 moveOffset = default, RotationDirection direction = RotationDirection.Clockwise)
        {
            bool collied = false;

            void revert(Piece piece)
            {
                // 회전 여부는 좌표 값 차이로 확인합니다.
                if (Precision.AlmostEquals(Vector2.Zero, moveOffset))
                {
                    if (direction == RotationDirection.Clockwise)
                        group.Rotate(RotationDirection.Counterclockwise);
                    else
                        group.Rotate(RotationDirection.Clockwise);

                    group.Pieces.ForEach(p => p.FlashColour(Color4.Red, 1000, Easing.OutQuint));

                    return;
                }

                piece.FlashColour(Color4.Red, 1000, Easing.OutQuint);
                group.MoveToOffset(-moveOffset);
            }

            foreach (var piece in group.Pieces)
            {
                foreach (var spacePiece in Children)
                {
                    if (spacePiece.Group.Equals(piece.Group))
                        continue;

                    if (spacePiece.Quad.Intersects(piece.Quad))
                    {
                        revert(piece);

                        collied = true;
                    }
                }

                if (piece.X < 0 || piece.X > 270 || piece.Y > 570)
                {
                    revert(piece);

                    collied = true;
                }
            }

            return collied;
        }
    }
}
