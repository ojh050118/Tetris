using System;
using System.Collections.Generic;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Utils;
using Tetris.Game.Pieces;

namespace Tetris.Game.Play
{
    public class RandomPieceGenerator
    {
        public Queue<PieceType> Bag { get; }

        public Queue<PieceType> PendingBag { get; }

        public RandomPieceGenerator()
        {
            Bag = new Queue<PieceType>();
            PendingBag = new Queue<PieceType>();
            fill();
        }

        public PieceType NextPiece()
        {
            if (Bag.Count == 0)
                fill();

            return Bag.Dequeue();
        }

        private void fill()
        {
            while (PendingBag.Count > 0)
                Bag.Enqueue(PendingBag.Dequeue());

            shuffle().ForEach(pieceType => PendingBag.Enqueue(pieceType));

            if (Bag.Count == 0)
                fill();
        }

        private PieceType[] shuffle()
        {
            List<PieceType> bag = new List<PieceType>();
            Func<PieceType> nextPiece = () => (PieceType)RNG.Next(0, 7);

            int count = 0;

            while (count < 7)
            {
                var pieceType = nextPiece.Invoke();

                if (bag.Contains(pieceType))
                    continue;

                bag.Add(pieceType);
                count++;
            }

            return bag.ToArray();
        }
    }
}
