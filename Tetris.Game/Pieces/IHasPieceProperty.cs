﻿using osuTK.Graphics;

namespace Tetris.Game.Pieces
{
    public interface IHasPieceProperty
    {
        bool[][] Shape { get; }

        PieceShape PieceType { get; }

        Color4 PieceColour { get; }
    }
}
