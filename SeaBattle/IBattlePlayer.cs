using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    interface IBattlePlayer
    {
        Point Turn(SeaBattleField ally, SeaBattleField enemy);
        void ReportEnd(bool victory);
        void SetShips(IEnumerable<Ship> ships, SeaBattleField field);
    }
}
