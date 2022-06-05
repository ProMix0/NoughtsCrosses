using CrossesZeroes.Abstractions;
using Microsoft.Extensions.Logging;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Игра в крестики-нолики человека с компьютером. Используется только в DI
    /// </summary>
    public class CrossesZeroesWithAi : CrossesZeroesGame
    {
        public CrossesZeroesWithAi(IRealPlayer player1, IAiPlayer player2, ICrossesZeroesField field, ILogger<CrossesZeroesWithAi> logger)
            : base(player1, player2, field, logger)
        { }
    }
}
