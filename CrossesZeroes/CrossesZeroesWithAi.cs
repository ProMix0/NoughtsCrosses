using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    class CrossesZeroesWithAi : CrossesZeroesGame
    {
        public CrossesZeroesWithAi(IRealPlayer player1, IAiPlayer player2, CrossesZeroesField field)
            : base(player1, player2, field)
        {
        }
    }
}
