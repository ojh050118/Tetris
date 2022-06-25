﻿using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;
using Tetris.Game.Pieces;
using Tetris.Game.Pieces.Group;

namespace Tetris.Game.Play
{
    public class PieceStage : Container<Piece>
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
                    addPieceGroup(rpg.NextPiece());
                    break;
            }

            checkForCollision();

            return base.OnKeyDown(e);
        }

        private void addPieceGroup(PieceType pieceType)
        {
            AddRange((group = createPieceGroup(pieceType)).Pieces);
        }

        private PieceGroup createPieceGroup(PieceType pieceType) => PieceGroupExtension.CreatePieceGroup(pieceType, new Vector2(Stage.STAGE_WIDTH / 2 - Piece.SIZE * 2, -Piece.SIZE * 2));

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
                        spacePiece.FlashColour(Color4.Red, 1000, Easing.OutQuint);
                        piece.FlashColour(ColourInfo.GradientVertical(Color4.Transparent, Color4.White.Opacity(0.5f)), 1000, Easing.OutQuint);

                        collied = true;
                    }
                }
            }

            return collied;
        }
    }
}
