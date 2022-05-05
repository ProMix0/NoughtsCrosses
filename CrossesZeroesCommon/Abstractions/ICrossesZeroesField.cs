namespace CrossesZeroes.Abstractions
{
    public interface ICrossesZeroesField
    {
        CellState this[int i, int j] { get; }

        int Width { get; }
        int Height { get; }

        ICrossesZeroesField AsReadonly();
        void Clear();
        bool IsEndGame(out CellState winner);
        void Set(Point point, CellState markType);
    }
}