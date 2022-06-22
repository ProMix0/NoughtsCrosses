using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using Microsoft.Extensions.Logging;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Реализация игры крестики-нолики
    /// </summary>
    public class Game : AbstractGame
    {
        /// <inheritdoc/>
        public Game(IPlayer player1, IPlayer player2, IGameField field,ILogger<Game> logger)
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

            await MakeTurn(CellState.Cross);

            if (gameCompleted) return false;

            await MakeTurn(CellState.Zero);

            return true;

            async Task MakeTurn(CellState player)
            {
                Point turnResult = await cross.Turn();
                field.Set(turnResult, player);
                zero.NotifyFieldChange(turnResult);
                cross.NotifyFieldChange(turnResult);
                CheckWin();
            }

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
