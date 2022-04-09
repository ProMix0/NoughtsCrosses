using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    class SeaBattle
    {
        private readonly IBattlePlayer player1;
        private readonly IBattlePlayer player2;

        private SeaBattleField field1 = new();
        private SeaBattleField field2 = new();

        private List<Ship> ships;

        public SeaBattle(IBattlePlayer player1, IBattlePlayer player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            ships = new()
            {
                new(),
                new()
            };
        }

        public void StartGame()
        {
            gameEnd = false;
            player1.SetShips(ships, field1);
            player1.SetShips(ships, field2);
        }

        bool gameEnd = false;

        public bool Turn()
        {
            if (!gameEnd)
            {
                field1.Shoot(player1.Turn(field1, field2));
                if (!field1.HaveShips())
                {
                    player1.ReportEnd(false);
                    player2.ReportEnd(true);
                    gameEnd = true;
                }
            }
            if (!gameEnd)
            {
                field2.Shoot(player2.Turn(field1, field2));
                if (!field2.HaveShips())
                {
                    player2.ReportEnd(false);
                    player1.ReportEnd(true);
                    gameEnd = true;
                }
            }

            return !gameEnd;
        }

    }
}
