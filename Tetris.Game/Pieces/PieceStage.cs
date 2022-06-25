using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;

namespace Tetris.Game.Pieces
{
    public class PieceStage : Container<Piece>
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
                case Key.X:
                case Key.Up:
                    group.Rotate(RotationDirection.Clockwise);
                    break;

                case Key.Z:
                    group.Rotate(RotationDirection.Counterclockwise);
                    break;

                case Key.A:
                    group.Rotate(RotationDirection.Clockwise);
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

            checkForCollision();

            return base.OnKeyDown(e);
        }

        private void addPieceGroup(PieceShape pieceType)
        {
            AddRange((group = createPieceGroup(pieceType)).Pieces);
        }

        private PieceGroup createPieceGroup(PieceShape pieceType) => PieceGroupExtension.CreatePieceGroup(pieceType, new Vector2(Stage.STAGE_WIDTH / 2 - Piece.SIZE * 2, -Piece.SIZE * 2));

        private bool checkForCollision()
        {
            bool collied = false;

            foreach (var piece in group.Pieces)
            {
                foreach (var spacePiece in Children)
                {
                    if (spacePiece.Group.Equals(piece.Group))
                        continue;

                    if (spacePiece.Quad.Intersects(piece.Quad))
                    {
                        spacePiece.FlashColour(Color4.White.Opacity(0), 500, Easing.OutQuint);
                        piece.FlashColour(Color4.White.Opacity(0), 500, Easing.OutQuint);

                        collied = true;
                    }
                }
            }

            return collied;
        }
    }
}
