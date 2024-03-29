﻿using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceI : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { false, false, false, false },
            new[] { true, true, true, true }
        };

        public override PieceType PieceType => PieceType.I;

        public override Color4 PieceColour => new Color4(52, 179, 132, 255);
    }
}
