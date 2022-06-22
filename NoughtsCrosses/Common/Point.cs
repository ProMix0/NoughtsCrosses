namespace NoughtsCrosses.Common
{
    /// <summary>
    /// Класс, представляющий координаты
    /// </summary>
    public struct Point
    {
        public readonly int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"X: {x}, Y: {y}";
        }
    }
}