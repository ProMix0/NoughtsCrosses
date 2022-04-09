using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle
{
    public class SeaBattleField
    {
        public FieldCellState[,] Field { get; }
        public int Size { get; internal set; }

        private List<PinnedShip> ships = new();

        internal void AddShip(Ship ship, Point position)
        {
            ships.Add(ship.AsPinned(position));
            for (int i = 0; i < ship.size.x; i++)
                for (int j = 0; j < ship.size.y; j++)
                    Field[i + position.x, j + position.y] = FieldCellState.Ship;
        }

        public bool HaveShips()
        {
            return ships.Count > 0;
        }

        internal void Shoot(Point point)
        {
            Field[point.x, point.y] |= (FieldCellState)2;
            foreach (var ship in ships)
            {
                for (int i = 0; i < ship.size.x; i++)
                    for (int j = 0; j < ship.size.y; j++)
                        if (i + ship.position.x == point.x && i + ship.position.y == point.y)
                        {
                            ship.body[i, j] = true;
                            if (ship.Destroyed())
                                ships.Remove(ship);
                            return;
                        }
            }
        }
    }
}