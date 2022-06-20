using System;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Graphics;
using osuTK;

namespace Tetris.Game.Pieces
{
    public static class PieceGroupExtension
    {
        public static void MoveTo(this PieceGroup group, Vector2 newPosition, double duration = 0, Easing easing = Easing.None)
        {
            foreach (var piece in group.Pieces)
                piece.MoveTo(newPosition, duration, easing);
        }

        public static void Rotate(this PieceGroup group, RotationDirection direction)
        {
            Vector2 centerPosition = getMinPosition(group.Pieces.First().Position);

            Vector2 getMinPosition(Vector2 value, int index = 0)
            {
                if (index == group.Pieces.Length)
                    return min(value, group.Pieces[index - 2].Position);

                var value2 = group.Pieces[index].Position;

                return min(value, getMinPosition(value2, ++index));
            }

            double rotateRadian = Math.PI / 2 * (direction == RotationDirection.Clockwise ? 1 : -1);

            switch (group.PieceType)
            {
                case PieceShape.O:
                    centerPosition += new Vector2(15, 15);
                    break;

                case PieceShape.I:
                    Vector2 offset = Vector2.Zero;
                    if (group.Rotation == 0 || group.Rotation == 360)
                        offset += new Vector2(60, 0);
                    else if (group.Rotation == 90 || group.Rotation == 270)
                        offset += new Vector2(0, 60);
                    else
                        offset += new Vector2(-60, 0);

                    centerPosition = group.Pieces.First().Position + offset;
                    break;

                case PieceShape.L:
                case PieceShape.J:
                case PieceShape.T:
                case PieceShape.Z:
                    centerPosition = group.Pieces[2].Position;
                    break;

                case PieceShape.S:
                    centerPosition = group.Pieces[3].Position;
                    break;

                default:
                    Debug.Assert(false, "알 수 없는 조각.");
                    centerPosition += Vector2.Zero;
                    break;
            }

            foreach (var piece in group.Pieces)
            {
                var newX = (float)((piece.X - centerPosition.X) * Math.Cos(rotateRadian) - (piece.Y - centerPosition.Y) * Math.Sin(rotateRadian));
                var newY = (float)((piece.X - centerPosition.X) * Math.Sin(rotateRadian) + (piece.Y - centerPosition.Y) * Math.Cos(rotateRadian));

                piece.MoveTo(centerPosition + new Vector2(newX, newY), 0, Easing.OutQuint);
            }

            group.Rotation += (int)MathHelper.RadiansToDegrees(rotateRadian);
            group.RotateCount++;
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
