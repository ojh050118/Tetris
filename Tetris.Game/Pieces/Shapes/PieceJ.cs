﻿using osuTK.Graphics;

namespace Tetris.Game.Pieces.Shapes
{
    public class PieceJ : Piece
    {
        public override bool[][] Shape => new[]
        {
            new[] { true, false, false, false },
            new[] { true, true, true, false }
        };

        public override PieceType PieceType => PieceType.J;

        public override Color4 PieceColour => new Color4(97, 79, 182, 255);
    }
}
