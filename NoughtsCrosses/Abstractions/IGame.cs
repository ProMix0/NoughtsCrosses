namespace NoughtsCrosses.Abstractions
{
    /// <summary>
    /// Represent noughts and crosses game
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Clear game state, restarting it
        /// </summary>
        void Restart();

        /// <summary>
        /// Turn method. Every player do single turn
        /// </summary>
        /// <returns>Can call <see cref="Turn"/> again</returns>
        Task<bool> Turn();

        /// <summary>
        /// Asking player if they want to repeat game
        /// </summary>
        /// <returns>True if both players want</returns>
        Task<bool> IsRestartWanted();
    }
}