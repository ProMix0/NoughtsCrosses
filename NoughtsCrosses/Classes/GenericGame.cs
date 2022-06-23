using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsCrosses.Classes
{
    public class Game<TPlayer1, TPlayer2> : Game
        where TPlayer1 : IPlayer
        where TPlayer2 : IPlayer
    {
        public Game(TPlayer1 player1, TPlayer2 player2, IGameField field, ILogger<Game> logger) : base(player1, player2, field, logger)
        {
        }
    }
}
