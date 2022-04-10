namespace CrossesZeroes
{
    public interface IPlayer
    {
        Point Turn(CrossesZeroesField field);
        void ReportEnd(bool victory);
        void SetMark(CellState mark);
    }
}