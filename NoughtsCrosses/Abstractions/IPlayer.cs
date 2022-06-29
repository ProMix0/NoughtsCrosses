using NoughtsCrosses.Common;

namespace NoughtsCrosses.Abstractions
{
    /// <summary>
    /// Represent game player
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Initialize player
        /// </summary>
        /// <param name="mark">Player's symbol in game</param>
        /// <param name="field">Reference to game field</param>
        void Init(CellState mark, IGameField field);

        /// <summary>
        /// Used by <see cref="IGame"/> to notify what cell was changed
        /// </summary>
        /// <param name="point">Index of changed cell</param>
        void NotifyFieldChange(Point point);

        /// <summary>
        /// Allows player to make his turn
        /// </summary>
        /// <returns>Index of cell which should be marked</returns>
        Task<Point> Turn();

        /// <summary>
        /// Used by <see cref="IGame"/> to notify game end
        /// </summary>
        /// <param name="victory">Does the player win</param>
        void ReportEnd(bool victory);

        /// <summary>
        /// Allows player to make decision if he want to play again
        /// </summary>
        /// <returns>True if want</returns>
        Task<bool> IsRepeatWanted();
    }
}