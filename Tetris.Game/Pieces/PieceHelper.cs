using System;
using System.Diagnostics;
using System.Linq;
using Tetris.Game.Pieces.Shapes;

namespace Tetris.Game.Pieces
{
    public static class PieceHelper
    {
        public static PieceType ToPieceShape(bool[][] shape)
        {
            int firstStartIndex = Array.IndexOf(shape.First(), true);
            int firstEndIndex = Array.LastIndexOf(shape.First(), true);

            int lastStartIndex = Array.IndexOf(shape.Last(), true);

            for (int i = 0; i < shape.Length; i++)
            {
                // 블럭이 연속하지 않는 조각은 없습니다.
                if (shape[i] == new bool[] { true, false, true, false })
                    break;

                if (shape[i] == new bool[] { false, true, false, true })
                    break;

                // 첫번째 열에서 블럭이 하나일때.
                // L, J, T 중 하나를 결정합니다.
                // 그렇지 않으면 블럭이 2개 이상 연속합니다.
                if (firstStartIndex == firstEndIndex)
                {
                    switch (firstStartIndex)
                    {
                        case -1:
                            if (lastStartIndex != -1)
                                return PieceType.I;

                            return PieceType.Other;

                        case 0:
                            return PieceType.J;

                        case 1:
                            return PieceType.T;

                        case 2:
                            return PieceType.L;
                    }
                }
                else
                {
                    // 블럭이 4개인 경우입니다.
                    if (firstEndIndex - firstStartIndex > 2)
                        return PieceType.I;

                    if (firstStartIndex < lastStartIndex)
                        return PieceType.Z;
                    else if (firstStartIndex > lastStartIndex)
                        return PieceType.S;
                    else
                        return PieceType.O;
                }
            }

            return PieceType.Other;
        }

        public static Piece GeneratePiece(PieceType pieceType)
        {
            switch (pieceType)
            {
                case PieceType.I:
                    return new PieceI();

                case PieceType.J:
                    return new PieceJ();

                case PieceType.L:
                    return new PieceL();

                case PieceType.O:
                    return new PieceO();

                case PieceType.S:
                    return new PieceS();

                case PieceType.T:
                    return new PieceT();

                case PieceType.Z:
                    return new PieceZ();

                default:
                    Debug.Assert(false, "알 수 없는 조각.");
                    return null;
            }
        }
    }
}
