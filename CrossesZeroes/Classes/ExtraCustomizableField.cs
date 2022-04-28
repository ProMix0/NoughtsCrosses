using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Classes
{
    public class ExtraCustomizableField : CustomizableField
    {
        public ExtraCustomizableField(IOptions<Configuration> config) : base(config)
        {
        }

        protected override IEnumerable<IEnumerable<Point>> WinIndexes()
        {
            //Вертикальные
            for (int i = Math.Max(0, LastPoint.x - winSequenceLength + 1); i < Math.Min(Height - winSequenceLength, LastPoint.x); i++)
                yield return GetCol(i, LastPoint.y);

            IEnumerable<Point> GetCol(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i + k, j);
            }

            //Горизонтальные
            for (int j = Math.Max(0, LastPoint.y - winSequenceLength); j < Math.Min(Width - winSequenceLength, LastPoint.y); j++)
                yield return GetRow(LastPoint.x, j);

            IEnumerable<Point> GetRow(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i, j + k);
            }

            //TODO
            //Диагональные возрастающие
            for (int k = Math.Min(Math.Min(LastPoint.y, Height - LastPoint.x - 1), winSequenceLength - 1);
                k > Math.Max(0, Math.Max(LastPoint.x, Width - LastPoint.y - 1)); k--)
                yield return GetUpDiagonal(LastPoint.x - k, LastPoint.y + k);

            IEnumerable<Point> GetUpDiagonal(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i - k, j + k);
            }

            //Диагональные убывающие
            for (int k = Math.Min(Math.Min(LastPoint.x, LastPoint.y), winSequenceLength - 1);
                k > Math.Max(0, Math.Max(Height - LastPoint.x, Width - LastPoint.y) - winSequenceLength); k--)
                yield return GetDownDiagonal(LastPoint.x - k, LastPoint.y - k);

            IEnumerable<Point> GetDownDiagonal(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i + k, j + k);
            }
        }

        private Point LastPoint => lastPoint.Value;
        private Point? lastPoint = null;
        public override void Set(Point point, CellState markType)
        {
            lastPoint = point;
            base.Set(point, markType);
        }
    }
}
