using NoughtsCrosses.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoughtsCrosses.Classes
{
    public class OptimizedField : CustomizableField
    {
        public OptimizedField(IOptions<Configuration> config, ILogger<OptimizedField> logger) : base(config, logger)
        {
        }

        protected override IEnumerable<IEnumerable<Point>> WinIndexes()
        {
            //Вертикальные
            for (int i = Math.Max(0, LastPoint.x - WinSequenceLength + 1); i <= Math.Min(Height - WinSequenceLength, LastPoint.x); i++)
                yield return GetCol(i, LastPoint.y);

            IEnumerable<Point> GetCol(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i + k, j);
            }

            //Горизонтальные
            for (int j = Math.Max(0, LastPoint.y - WinSequenceLength); j <= Math.Min(Width - WinSequenceLength, LastPoint.y); j++)
                yield return GetRow(LastPoint.x, j);

            IEnumerable<Point> GetRow(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i, j + k);
            }

            //Диагональные возрастающие
            for (int k = 0; k < WinSequenceLength; k++)
            {
                IEnumerable<Point> seq = GetUpDiagonal(LastPoint.x + k, LastPoint.y - k);
                if (seq.All(point => point.x >= 0 && point.y >= 0 && point.x < Height && point.y < Width))
                    yield return seq;
            }

            IEnumerable<Point> GetUpDiagonal(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i - k, j + k);
            }

            //Диагональные убывающие
            for (int k = 0; k < WinSequenceLength; k++)
            {
                IEnumerable<Point> seq = GetDownDiagonal(LastPoint.x - k, LastPoint.y - k);
                if (seq.All(point => point.x >= 0 && point.y >= 0 && point.x < Height && point.y < Width))
                    yield return seq;
            }

            IEnumerable<Point> GetDownDiagonal(int i, int j)
            {
                for (int k = 0; k < WinSequenceLength; k++)
                    yield return new(i + k, j + k);
            }
        }

        private Point LastPoint => lastPoint!.Value;
        private Point? lastPoint = null;
        public override void Set(Point point, CellState markType)
        {
            lastPoint = point;
            base.Set(point, markType);
        }
    }
}
