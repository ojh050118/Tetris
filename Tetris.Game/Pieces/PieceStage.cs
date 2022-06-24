using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
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
            group = createPieceGroup(PieceShape.I, Vector2.Zero);
            AddRange(group.Pieces);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.Up)
                group.Rotate(RotationDirection.Clockwise);

            return base.OnKeyDown(e);
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
                        piece.Position = new Vector2(currentPosition, index * 30);

                        group.Add(piece);
                    }

                    currentPosition += 30;
                }

                currentPosition = 0;
            }

            return new PieceGroup(group.ToArray())
            {
                Position = position
            };
        }
    }
}
