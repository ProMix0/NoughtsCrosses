namespace NoughtsCrosses.Common
{
    /// <summary>
    /// Basic two indexes class
    /// </summary>
    public readonly struct Point
    {
        public readonly int X, Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}