using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Inherit <see cref="Game"/>. Useful to DI
    /// </summary>
    /// <typeparam name="TPlayer1">Player one type</typeparam>
    /// <typeparam name="TPlayer2">Player two type</typeparam>
    public class Game<TPlayer1, TPlayer2> : Game
        where TPlayer1 : IPlayer
        where TPlayer2 : IPlayer
    {
        public Game(TPlayer1 player1, TPlayer2 player2, IGameField field, ILogger<Game> logger) : base(player1, player2,
            field, logger)
        {
        }
    }
}