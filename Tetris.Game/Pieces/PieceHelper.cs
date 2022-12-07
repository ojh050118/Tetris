using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace Tetris.Game.Pieces
{
    public static class PieceHelper
    {
        public static bool IsCollideWithBoundary(this Piece piece, int width, int height = 20)
        {
            Quad leftBoundary = new Quad(-10, 0, Block.SIZE - 0.1f, Block.SIZE * height - 0.1f);
            Quad rightBoundary = new Quad(width * Block.SIZE + 0.1f, 0, Block.SIZE, Block.SIZE * height);

            foreach (var block in piece.Blocks)
            {
                if (block.Quad.Intersects(leftBoundary) || block.Quad.Intersects(rightBoundary))
                    return true;
            }

            return false;
        }

        public static Vector2 ToParentPosition(this Block block, int depth)
        {
            Vector2 pos = Vector2.Zero;
            Drawable childDrawable = block;
            Drawable drawable = block.Parent;

            for (int i = 0; i < depth; i++)
            {
                pos.X += drawable.AnchorPosition.X - drawable.OriginPosition.X + drawable.X + childDrawable.X;
                pos.Y += drawable.AnchorPosition.Y - drawable.OriginPosition.Y + drawable.Y + childDrawable.Y;

                childDrawable = drawable;
                drawable = drawable.Parent;
            }

            return pos;
        }
    }
}
