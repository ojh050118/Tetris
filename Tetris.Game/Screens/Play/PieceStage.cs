using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;
using Tetris.Game.Input;
using Tetris.Game.Pieces;
using Tetris.Game.Pieces.Group;

namespace Tetris.Game.Screens.Play
{
    public class PieceStage : Container<Piece>, IKeyBindingHandler<InputAction>
    {
        public PieceGroup CurrentPieceGroup;

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
                    CurrentPieceGroup.Rotate(direction = RotationDirection.Clockwise);
                    break;

                case InputAction.RotateCounterclockwise:
                    CurrentPieceGroup.Rotate(direction = RotationDirection.Counterclockwise);
                    break;

                case InputAction.Rotate180:
                    CurrentPieceGroup.Rotate(direction = RotationDirection.Clockwise);
                    checkForCollision(moveOffset, direction);
                    CurrentPieceGroup.Rotate(direction = RotationDirection.Clockwise);
                    break;

                case InputAction.PieceLeft:
                    CurrentPieceGroup.MoveToOffset(moveOffset = new Vector2(-Piece.SIZE, 0));
                    break;

                case InputAction.PieceRight:
                    CurrentPieceGroup.MoveToOffset(moveOffset = new Vector2(Piece.SIZE, 0));
                    break;

                case InputAction.SoftDrop:
                    drop(false);
                    break;

                case InputAction.HardDrop:
                    drop(true);
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

        private void drop(bool hardDrop)
        {
            if (hardDrop)
            {
                while (!checkForCollision(new Vector2(0, Piece.SIZE)))
                    CurrentPieceGroup.MoveToOffset(new Vector2(0, Piece.SIZE));

                commit();
            }
            else
            {
                CurrentPieceGroup.MoveToOffset(new Vector2(0, Piece.SIZE));

                if (checkForCollision(new Vector2(0, Piece.SIZE)))
                    commit();
            }
        }

        private void commit()
        {
            (int count, int startPosY) = clearLine();

            Children.ForEach(p =>
            {
                if (startPosY < p.Y)
                    return;

                var offset = p.Position + new Vector2(0, Piece.SIZE * count);
                p.MoveToOffset(new Vector2(0, Piece.SIZE * count));
                p.Quad = new Quad(offset.X, offset.Y, p.Width - 1, p.Height - 1);
            });
        }

        private (int clearedLine, int StartLineY) clearLine()
        {
            int clearedLine = 0;
            int startLineY = 0;

            for (int height = 570; height >= 0; height -= Piece.SIZE)
            {
                var line = Array.FindAll(Children.ToArray(), p => (int)p.Y == height);

                if (line.Length == 10)
                {
                    startLineY = (int)line.First().Y;
                    line.ForEach(p =>
                    {
                        Remove(p);
                        p.Dispose();
                    });
                    clearedLine++;
                }
            }

            return (clearedLine, startLineY);
        }

        private void addPieceGroup(PieceType pieceType)
        {
            AddRange((CurrentPieceGroup = createPieceGroup(pieceType)).Pieces);
        }

        private PieceGroup createPieceGroup(PieceType pieceType) => PieceGroupExtension.CreatePieceGroup(pieceType, new Vector2(Stage.STAGE_WIDTH / 2 - Piece.SIZE * 2, -Piece.SIZE * 2));

        private bool checkForCollision(Vector2 moveOffset = default, RotationDirection direction = RotationDirection.Clockwise)
        {
            bool collied = false;

            void revert()
            {
                // 회전 여부는 좌표 값 차이로 확인합니다.
                if (Precision.AlmostEquals(Vector2.Zero, moveOffset))
                {
                    CurrentPieceGroup.Rotate(direction == RotationDirection.Clockwise ? RotationDirection.Counterclockwise : RotationDirection.Clockwise);

                    CurrentPieceGroup.Pieces.ForEach(p => p.FlashColour(Color4.Red, 1000, Easing.OutQuint));

                    return;
                }

                CurrentPieceGroup.Pieces.ForEach(p => p.FlashColour(Color4.Red, 1000, Easing.OutQuint));
                CurrentPieceGroup.MoveToOffset(-moveOffset);
            }

            foreach (var piece in CurrentPieceGroup.Pieces)
            {
                foreach (var spacePiece in Children)
                {
                    if (spacePiece.Group.Equals(CurrentPieceGroup))
                        continue;

                    if (spacePiece.Quad.Intersects(piece.Quad))
                        collied = true;
                }

                if ((int)piece.X < 0 || (int)piece.X > 270 || (int)piece.Y > 570)
                    collied = true;
            }

            if (collied)
                revert();

            return collied;
        }
    }
}
