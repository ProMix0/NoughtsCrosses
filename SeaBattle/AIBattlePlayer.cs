using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    class AIBattlePlayer : IBattlePlayer
    {
        public void ReportEnd(bool victory)
        {
            
        }

        public void SetShips(IEnumerable<Ship> ships, SeaBattleField field)
        {
            //TODO prevent ships overlaying
            Random random = new();
            foreach (var ship in ships)
            {
                field.AddShip(ship, new Point(random.Next(field.Size), random.Next(field.Size)));
            }
        }

        int i = 0, j = 0;

        public Point Turn(SeaBattleField ally, SeaBattleField enemy)
        {
            Point point = new(i, j);
            if (j >= enemy.Size)
            {
                i++; j = 0;
            }
            return point;
        }
    }
}
