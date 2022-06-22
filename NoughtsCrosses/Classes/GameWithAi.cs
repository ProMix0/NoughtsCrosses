using NoughtsCrosses.Abstractions;
using Microsoft.Extensions.Logging;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Игра в крестики-нолики человека с компьютером. Используется только в DI
    /// </summary>
    public class GameWithAi : Game
    {
        public GameWithAi(IRealPlayer player1, IAiPlayer player2, IGameField field, ILogger<GameWithAi> logger)
            : base(player1, player2, field, logger)
        { }
    }
}
