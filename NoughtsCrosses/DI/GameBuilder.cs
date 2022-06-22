using NoughtsCrosses.Abstractions;

namespace NoughtsCrosses.DI
{
    public class GameBuilder
    {
        internal Type? player1, player2, field, game;

        internal bool IsValid(out Exception? ex)
        {
            if (player1 is null || player2 is null)
            {
                ex = new InvalidOperationException("Players must be specificated");
                return false;
            }
            if (field is null)
            {
                ex = new InvalidOperationException("Field must be specificated");
                return false;
            }
            if (game is null)
            {
                ex = new InvalidOperationException("Game must be specificated");
                return false;
            }

            ex = null;
            return true;
        }

        public GameBuilder UsePlayers<TPlayer1, TPlayer2>()
            where TPlayer1 : IPlayer
            where TPlayer2 : IPlayer
        {
            player1 = typeof(TPlayer1);
            player2 = typeof(TPlayer2);

            return this;
        }

        public GameBuilder UseField<TField>()
            where TField : IGameField
        {
            field = typeof(TField);

            return this;
        }

        public GameBuilder UseGame<TGame>()
            where TGame : IGame
        {
            game = typeof(TGame);

            return this;
        }
    }
}
