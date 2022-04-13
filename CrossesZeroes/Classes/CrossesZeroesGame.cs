using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    /// <summary>
    /// Реализация игры крестики-нолики
    /// </summary>
    class CrossesZeroesGame : CrossesZeroesAbstract
    {
        /// <inheritdoc/>
        public CrossesZeroesGame(IPlayer player1, IPlayer player2, CrossesZeroesField field)
            ///Вызов конструктора базового класса с параметрами
            : base(player1, player2, field)
        {
            cross.SetMark(CellState.Cross);
            zero.SetMark(CellState.Zero);
        }

        private bool gameCompleted = false;

        /// <inheritdoc/>
        public override bool Turn()
        {
            if (gameCompleted) return false;
            field.Set(cross.Turn(field.AsReadonly()), CellState.Cross);
            CheckWin();

            if (gameCompleted) return false;
            field.Set(zero.Turn(field.AsReadonly()), CellState.Zero);
            CheckWin();

            return true;

            //Проверка на выигрыш
            void CheckWin()
            {
                if (field.IsEndGame(out CellState winner))
                {
                    switch (winner)
                    {
                        case CellState.Empty:
                            cross.ReportEnd(false);
                            zero.ReportEnd(false);
                            gameCompleted = true;
                            break;
                        case CellState.Cross:
                            cross.ReportEnd(true);
                            zero.ReportEnd(false);
                            gameCompleted = true;
                            break;
                        case CellState.Zero:
                            cross.ReportEnd(false);
                            zero.ReportEnd(true);
                            gameCompleted = true;
                            break;
                    }
                }
            }
        }

        public override void Restart()
        {
            gameCompleted = false;

            //Смена знака игроков
            IPlayer temp = cross;
            cross = zero;
            zero = temp;

            cross.SetMark(CellState.Cross);
            zero.SetMark(CellState.Zero);

            field.Clear();
        }
    }
}
