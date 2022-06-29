using NoughtsCrosses.Classes;
using NoughtsCrosses.Common;

namespace NoughtsCrosses.Abstractions
{
    public interface IGameField
    {
        /// <summary>
        /// Indexer to access field values
        /// </summary>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns>State of requested cell</returns>
        CellState this[int i, int j] { get; }

        /// <summary>
        /// Columns count
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Rows count
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Get instance to read access
        /// </summary>
        /// <returns>Real-time updatable instance of field</returns>
        ReadonlyField AsReadonly();

        /// <summary>
        /// Clears field
        /// </summary>
        void Clear();

        /// <summary>
        /// Check field state to game's end
        /// </summary>
        /// <param name="winner">Symbol of winning player.<para/>
        /// <see cref="CellState.Empty"/> if drawn game
        /// </param>
        /// <returns>Game was ended</returns>
        bool IsEndGame(out CellState winner);

        /// <summary>
        /// Set state of choose cell
        /// </summary>
        /// <param name="point">Choose cell indexes</param>
        /// <param name="markType">Needed mark type</param>
        void Set(Point point, CellState markType);
    }
}