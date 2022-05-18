using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Реализация игры крестики-нолики
    /// </summary>
    public class CrossesZeroesGame : CrossesZeroesAbstract
    {
        /// <inheritdoc/>
        public CrossesZeroesGame(IPlayer player1, IPlayer player2, ICrossesZeroesField field)
            ///Вызов конструктора базового класса с параметрами
            : base(player1, player2, field)
        {
            cross.Init(CellState.Cross);
            zero.Init(CellState.Zero);
        }

        private bool gameCompleted = false;

        /// <inheritdoc/>
        public async override Task<bool> Turn()
        {
            if (gameCompleted) return false;
            field.Set(await cross.Turn(field.AsReadonly()), CellState.Cross);
            CheckWin();

            if (gameCompleted) return false;
            field.Set(await zero.Turn(field.AsReadonly()), CellState.Zero);
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
                            cross.ReportEnd(false, field);
                            zero.ReportEnd(false, field);
                            gameCompleted = true;
                            break;
                        case CellState.Cross:
                            cross.ReportEnd(true, field);
                            zero.ReportEnd(false, field);
                            gameCompleted = true;
                            break;
                        case CellState.Zero:
                            cross.ReportEnd(false, field);
                            zero.ReportEnd(true, field);
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
            (zero, cross) = (cross, zero);

            cross.Init(CellState.Cross);
            zero.Init(CellState.Zero);

            field.Clear();
        }
    }
}
