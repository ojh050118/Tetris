using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osuTK;
using osuTK.Input;

namespace Tetris.Game.Pieces
{
    public class PieceStage : Container
    {
        private PieceGroup group;

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            Size = new Vector2(Stage.STAGE_WIDTH, Stage.STAGE_HEIGHT);
            addPieceGroup((PieceShape)RNG.Next(0, 7));
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    group.Rotate(RotationDirection.Clockwise);
                    break;

                case Key.Left:
                    group.MoveToOffset(new Vector2(-Piece.SIZE, 0));
                    break;

                case Key.Right:
                    group.MoveToOffset(new Vector2(Piece.SIZE, 0));
                    break;

                case Key.Down:
                    group.MoveToOffset(new Vector2(0, Piece.SIZE));
                    break;

                case Key.Space:
                    addPieceGroup((PieceShape)RNG.Next(0, 7));
                    break;
            }

            return base.OnKeyDown(e);
        }

        private void addPieceGroup(PieceShape pieceType)
        {
            AddRange((group = createPieceGroup(pieceType, new Vector2(Stage.STAGE_WIDTH / 2 - Piece.SIZE * 2, -Piece.SIZE * 2))).Pieces);
        }

        private PieceGroup createPieceGroup(PieceShape pieceType, Vector2 position)
        {
            bool[][] shape = PieceHelper.GeneratePiece(pieceType).Shape;
            int currentPosition = 0;
            List<Piece> group = new List<Piece>();

            for (var index = 0; index < shape.Length; index++)
            {
                var row = shape[index];

                for (var i = 0; i < row.Length; i++)
                {
                    if (row[i])
                    {
                        var piece = PieceHelper.GeneratePiece(pieceType);
                        piece.InitialPosition = new Vector2(currentPosition, index * Piece.SIZE);
                        group.Add(piece);
                    }

                    currentPosition += Piece.SIZE;
                }

                currentPosition = 0;
            }

            var pieceGroup = new PieceGroup(group.ToArray());
            pieceGroup.SetDefaultPiecePosition(position);

            return pieceGroup;
        }
    }
}
