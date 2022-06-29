using Microsoft.Extensions.Logging;
using NoughtsCrosses.Common;

namespace NoughtsCrosses.Abstractions
{
    /// <summary>
    /// Base class for realizations
    /// </summary>
    public abstract class AbstractGame : IGame
    {
        protected readonly IGameField field;
        protected readonly ILogger<AbstractGame> logger;

        /// <summary>
        /// Cross player
        /// </summary>
        protected IPlayer cross;

        protected bool gameCompleted;

        /// <summary>
        /// Nought player
        /// </summary>
        protected IPlayer zero;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player1">First player</param>
        /// <param name="player2">Second player</param>
        /// <param name="field">Game field</param>
        /// <param name="logger">Logger</param>
        public AbstractGame(IPlayer player1, IPlayer player2, IGameField field, ILogger<AbstractGame> logger)
        {
            cross = player1;
            zero = player2;
            this.field = field;
            this.logger = logger;

            cross.Init(CellState.Cross, field.AsReadonly());
            zero.Init(CellState.Zero, field.AsReadonly());
        }

        public virtual void Restart()
        {
            gameCompleted = false;

            // Swap players symbols
            (zero, cross) = (cross, zero);

            cross.Init(CellState.Cross, field.AsReadonly());
            zero.Init(CellState.Zero, field.AsReadonly());

            field.Clear();
        }

        public abstract Task<bool> Turn();


        public virtual async Task<bool> IsRestartWanted()
        {
            Task<bool> crossTask = cross.IsRepeatWanted();
            Task<bool> zeroTask = zero.IsRepeatWanted();
            return await zeroTask & await crossTask;
        }
    }
}