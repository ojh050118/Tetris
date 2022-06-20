using System;
using System.Linq;

namespace Tetris.Game.Pieces
{
    public static class PieceHelper
    {

        public static PieceShape ToPieceShape(bool[][] shape)
        {
            int firstStartIndex = Array.IndexOf(shape.First(), true);
            int firstEndIndex = Array.LastIndexOf(shape.First(), true);

            int lastStartIndex = Array.IndexOf(shape.Last(), true);
            //int lastEndIndex = Array.LastIndexOf(shape.Last(), true);

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
                                return PieceShape.I;

                            return PieceShape.Other;

                        case 0:
                            return PieceShape.J;

                        case 1:
                            return PieceShape.T;

                        case 2:
                            return PieceShape.L;
                    }
                }
                else
                {
                    // 블럭이 4개인 경우입니다.
                    if (firstEndIndex - firstStartIndex > 2)
                        return PieceShape.I;

                    if (firstStartIndex < lastStartIndex)
                        return PieceShape.Z;
                    else if (firstStartIndex > lastStartIndex)
                        return PieceShape.S;
                    else
                        return PieceShape.O;
                }
            }

            return PieceShape.Other;
        }

        public static Piece GeneratePiece(PieceShape pieceType)
        {
            switch (pieceType)
            {
                case PieceShape.I:
                    return new PieceI();

                case PieceShape.J:
                    return new PieceJ();

                case PieceShape.L:
                    return new PieceL();

                case PieceShape.O:
                    return new PieceO();

                case PieceShape.S:
                    return new PieceS();

                case PieceShape.T:
                    return new PieceT();

                case PieceShape.Z:
                    return new PieceZ();

                default:
                    return null;
            }
        }
    }
}
