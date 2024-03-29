﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace Tetris.Game.Pieces.Group
{
    public static class PieceGroupExtension
    {
        private static int groupID;

        public static void MoveToOffset(this PieceGroup group, Vector2 newPosition, double duration = 0, Easing easing = Easing.None)
        {
            group.Position += newPosition;

            foreach (var piece in group.Pieces)
            {
                var offset = piece.Position + newPosition;
                piece.MoveTo(offset, duration, easing);
                piece.Quad = new Quad(offset.X, offset.Y, piece.Size.X - 1, piece.Size.Y - 1);
            }
        }

        public static void Rotate(this PieceGroup group, RotationDirection direction)
        {
            Vector2 getMinPosition(Vector2 value, int index = 0)
            {
                if (index == group.Pieces.Count)
                    return min(value, group.Pieces[index - 2].Position);

                var value2 = group.Pieces[index].Position;

                return min(value, getMinPosition(value2, ++index));
            }

            Vector2 centerPosition = getMinPosition(group.Pieces.First().Position);
            double rotateRadian = Math.PI / 2 * (direction == RotationDirection.Clockwise ? 1 : -1);
            Vector2 offset = Vector2.Zero;

            group.Rotation += (int)MathHelper.RadiansToDegrees(rotateRadian);
            group.RotateCount++;

            switch (group.PieceType)
            {
                case PieceType.O:
                    var signX = Math.Sign(centerPosition.X) >= 0 ? 1 : -1;
                    var signY = Math.Sign(centerPosition.Y) >= 0 ? 1 : -1;
                    var sign = Math.Sign(centerPosition.X) * Math.Sign(centerPosition.X) >= 0 ? 1 : -1;

                    centerPosition += sign * new Vector2(Piece.SIZE / 2 * signX, Piece.SIZE / 2 * signY);
                    break;

                case PieceType.I:
                    centerPosition = direction == RotationDirection.Clockwise ? group.Pieces[2].Position : group.Pieces[1].Position;

                    switch (Math.Abs(group.Rotation))
                    {
                        case 90:
                            offset += Math.Sign(rotateRadian) * new Vector2(0, Piece.SIZE);
                            break;

                        case 180:
                            offset += Math.Sign(rotateRadian) * new Vector2(-Piece.SIZE, 0);
                            break;

                        case 270:
                            offset += Math.Sign(rotateRadian) * new Vector2(0, -Piece.SIZE);
                            break;

                        case 0:
                        case 360:
                            offset += Math.Sign(rotateRadian) * new Vector2(Piece.SIZE, 0);
                            break;
                    }

                    break;

                case PieceType.L:
                case PieceType.J:
                case PieceType.T:
                case PieceType.Z:
                    centerPosition = group.Pieces[2].Position;
                    break;

                case PieceType.S:
                    centerPosition = group.Pieces[3].Position;
                    break;

                default:
                    Debug.Assert(false, "알 수 없는 조각.");
                    break;
            }

            foreach (var piece in group.Pieces)
            {
                var newX = (float)((piece.X - centerPosition.X) * Math.Cos(rotateRadian) - (piece.Y - centerPosition.Y) * Math.Sin(rotateRadian));
                var newY = (float)((piece.X - centerPosition.X) * Math.Sin(rotateRadian) + (piece.Y - centerPosition.Y) * Math.Cos(rotateRadian));

                piece.Position = centerPosition + new Vector2(newX, newY) + offset;
                piece.Quad = new Quad(piece.Position.X, piece.Position.Y, piece.Size.X - 1, piece.Size.Y - 1);
            }
        }

        public static PieceGroup CreatePieceGroup(PieceType pieceType, Vector2 position)
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
                        piece.Quad = new Quad(currentPosition, index * Piece.SIZE, Piece.SIZE, Piece.SIZE);
                        group.Add(piece);
                    }

                    currentPosition += Piece.SIZE;
                }

                currentPosition = 0;
            }

            var pieceGroup = new PieceGroup(groupID++, group);
            pieceGroup.SetDefaultPiecePosition(position);

            return pieceGroup;
        }

        private static Vector2 min(Vector2 val1, Vector2 val2)
        {
            var val1Distance = Math.Sqrt(val1.X * val1.X + val1.Y * val1.Y);
            var val2Distance = Math.Sqrt(val2.X * val2.X + val2.Y * val2.Y);

            if (val1Distance < val2Distance)
                return val1;
            else
                return val2;
        }
    }
}
