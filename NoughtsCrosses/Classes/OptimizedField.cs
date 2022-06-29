using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NoughtsCrosses.Common;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Field with more efficient win check due to last changed cell based checks
    /// </summary>
    public class OptimizedField : CustomizableField
    {
        private Point? lastPoint;

        public OptimizedField(IOptions<Configuration> config, ILogger<OptimizedField> logger) : base(config, logger)
        {
        }

        private Point LastPoint => lastPoint!.Value;

        protected override IEnumerable<IEnumerable<Point>> WinIndexes()
        {
            //Vertical
            for (int i = Math.Max(0, LastPoint.X - WinSequenceLength + 1);
                 i <= Math.Min(Height - WinSequenceLength, LastPoint.X);
                 i++)
                yield return GetCol(i, LastPoint.Y);

            IEnumerable<Point> GetCol(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i + k, j);
            }

            //Horizontal
            for (int j = Math.Max(0, LastPoint.Y - WinSequenceLength);
                 j <= Math.Min(Width - WinSequenceLength, LastPoint.Y);
                 j++)
                yield return GetRow(LastPoint.X, j);

            IEnumerable<Point> GetRow(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i, j + k);
            }

            //Diagonal up 
            for (int k = 0; k < WinSequenceLength; k++)
            {
                IEnumerable<Point> seq = GetUpDiagonal(LastPoint.X + k, LastPoint.Y - k);
                if (seq.All(point => point.X >= 0 && point.Y >= 0 && point.X < Height && point.Y < Width))
                    yield return seq;
            }

            IEnumerable<Point> GetUpDiagonal(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i - k, j + k);
            }

            //Diagonal down
            for (int k = 0; k < WinSequenceLength; k++)
            {
                IEnumerable<Point> seq = GetDownDiagonal(LastPoint.X - k, LastPoint.Y - k);
                if (seq.All(point => point.X >= 0 && point.Y >= 0 && point.X < Height && point.Y < Width))
                    yield return seq;
            }

            IEnumerable<Point> GetDownDiagonal(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i + k, j + k);
            }
        }

        public override void Set(Point point, CellState markType)
        {
            lastPoint = point;
            base.Set(point, markType);
        }
    }
}