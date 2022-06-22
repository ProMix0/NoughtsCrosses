using NoughtsCrosses.Classes;
using NoughtsCrosses.Common;

namespace NoughtsCrosses.Abstractions
{
    public interface IGameField
    {
        CellState this[int i, int j] { get; }

        int Width { get; }
        int Height { get; }

        ReadonlyField AsReadonly();
        void Clear();
        bool IsEndGame(out CellState winner);
        void Set(Point point, CellState markType);
    }
}