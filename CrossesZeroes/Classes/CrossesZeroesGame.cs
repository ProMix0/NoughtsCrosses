using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using Microsoft.Extensions.Logging;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Реализация игры крестики-нолики
    /// </summary>
    public class CrossesZeroesGame : CrossesZeroesAbstract
    {
        /// <inheritdoc/>
        public CrossesZeroesGame(IPlayer player1, IPlayer player2, ICrossesZeroesField field,ILogger<CrossesZeroesGame> logger)
            ///Вызов конструктора базового класса с параметрами
            : base(player1, player2, field,logger)
        {
            cross.Init(CellState.Cross, field.AsReadonly());
            zero.Init(CellState.Zero, field.AsReadonly());
        }

        private bool gameCompleted = false;

        /// <inheritdoc/>
        public async override Task<bool> Turn()
        {
            if (gameCompleted) return false;
            Point turnResult = await cross.Turn();
            field.Set(turnResult, CellState.Cross);
            zero.NotifyFieldChange(turnResult);
            cross.NotifyFieldChange(turnResult);
            CheckWin();

            if (gameCompleted) return false;
            turnResult = await zero.Turn();
            field.Set(turnResult, CellState.Zero);
            cross.NotifyFieldChange(turnResult);
            zero.NotifyFieldChange(turnResult);
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
            (zero, cross) = (cross, zero);

            cross.Init(CellState.Cross, field.AsReadonly());
            zero.Init(CellState.Zero, field.AsReadonly());

            field.Clear();
        }
    }
}
