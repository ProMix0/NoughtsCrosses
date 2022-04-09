using System;

namespace SeaBattle
{
    public class PinnedShip : Ship
    {
        public PinnedShip(Point position, Ship ship)
            : base(ship)
        {
            this.position = position;
            body = new bool[size.x, size.y];
        }
        public readonly bool[,] body;

        public Point position;

        internal bool Destroyed()
        {
            for (int i = 0; i < body.GetLength(0); i++)
                for (int j = 0; j < body.GetLength(1); j++)
                    if (!body[i, j]) return false;
            return true;
        }
    }
}