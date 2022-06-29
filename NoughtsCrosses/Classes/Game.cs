using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Basic realisation
    /// </summary>
    public class Game : AbstractGame
    {
        public Game(IPlayer player1, IPlayer player2, IGameField field, ILogger<Game> logger)
            : base(player1, player2, field, logger)
        {
        }

        public async override Task<bool> Turn()
        {
            if (gameCompleted) return false;

            await MakeTurn(CellState.Cross);

            if (gameCompleted) return false;

            await MakeTurn(CellState.Zero);

            return true;

            // Allows player with specified symbol make turn
            // And notify both players about that
            async Task MakeTurn(CellState player)
            {
                Point turnResult = await (player == CellState.Cross ? cross : zero).Turn();
                field.Set(turnResult, player);
                zero.NotifyFieldChange(turnResult);
                cross.NotifyFieldChange(turnResult);
                CheckWin();
            }

            // Check win, notify players and set game state to completed if true
            void CheckWin()
            {
                if (field.IsEndGame(out CellState winner))
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
}