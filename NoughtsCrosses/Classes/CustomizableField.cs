using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using NoughtsCrosses.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoughtsCrosses.Classes
{
    public class CustomizableField : GameField
    {

        public CustomizableField(IOptions<Configuration> config, ILogger<CustomizableField> logger)
            : this(config.Value.Height, config.Value.Width, config.Value.WinSequenceLength, logger)
        { }

        private CustomizableField(int height, int width, int winSequenceLength, ILogger<CustomizableField> logger)
            :base(logger)
        {
            Width = width;
            Height = height;
            WinSequenceLength = winSequenceLength;

            Clear();
        }

        public override int Width
        {
            get => width;
            protected set
            {
                if (value <= 0) throw logger.LogExceptionMessage(new ArgumentException("Must be more than zero", nameof(Width)));
                width = value;
            }
        }
        private int width;

        public override int Height
        {
            get => height;
            protected set
            {
                if (value <= 0) throw logger.LogExceptionMessage(new ArgumentException("Must be more than zero", nameof(Height)));
                height = value;
            }
        }
        private int height;

        protected int WinSequenceLength
        {
            get => winSequenceLength; set
            {
                if (value <= 0) throw logger.LogExceptionMessage(new ArgumentException("Must be more than zero", nameof(WinSequenceLength)));
                winSequenceLength = value;
            }
        }
        private int winSequenceLength;

        public override void Clear()
        {
            field = new CellState[Height, Width];
        }
        protected override IEnumerable<IEnumerable<Point>> WinIndexes() => winIndexesCache ??= GenerateWinIndexes();


        private List<List<Point>>? winIndexesCache = null;
        private List<List<Point>> GenerateWinIndexes()
        {
            List<List<Point>> result = new();

            //Вертикальные
            VerticalIndexes(result);

            //Горизонтальные
            HorizontalIndexes(result);

            //Диагональные возрастающие
            DiagonalUpIndexes(result);

            //Диагональные убывающие
            DiagonalDownIndexes(result);

            return result;

            void VerticalIndexes(List<List<Point>> result)
            {
                for (int i = 0; i <= field.GetLength(0) - WinSequenceLength; i++)
                    for (int j = 0; j < field.GetLength(1); j++)
                        GetCol(i, j);

                void GetCol(int i, int j)
                {
                    List<Point> temp = new();

                    for (int k = 0; k < WinSequenceLength; k++)
                        temp.Add(new(i + k, j));

                    result.Add(temp);
                }
            }

            void HorizontalIndexes(List<List<Point>> result)
            {
                for (int i = 0; i < field.GetLength(0); i++)
                    for (int j = 0; j <= field.GetLength(1) - WinSequenceLength; j++)
                        GetRow(i, j);

                void GetRow(int i, int j)
                {
                    List<Point> temp = new();

                    for (int k = 0; k < WinSequenceLength; k++)
                        temp.Add(new(i, j + k));

                    result.Add(temp);
                }
            }

            void DiagonalUpIndexes(List<List<Point>> result)
            {
                for (int i = WinSequenceLength - 1; i < field.GetLength(0); i++)
                    for (int j = 0; j <= field.GetLength(1) - WinSequenceLength; j++)
                        GetUpDiagonal(i, j);

                void GetUpDiagonal(int i, int j)
                {
                    List<Point> temp = new();

                    for (int k = 0; k < WinSequenceLength; k++)
                        temp.Add(new(i - k, j + k));

                    result.Add(temp);
                }
            }

            void DiagonalDownIndexes(List<List<Point>> result)
            {
                for (int i = 0; i <= field.GetLength(0) - WinSequenceLength; i++)
                    for (int j = 0; j <= field.GetLength(1) - WinSequenceLength; j++)
                        GetDownDiagonal(i, j);

                void GetDownDiagonal(int i, int j)
                {
                    List<Point> temp = new();
                    for (int k = 0; k < WinSequenceLength; k++)
                        temp.Add(new(i + k, j + k));
                    result.Add(temp);
                }
            }
        }

        public class Configuration
        {
            public static readonly string SectionName = nameof(CustomizableField);

            public int Width { get; set; }
            public int Height { get; set; }
            public int WinSequenceLength { get; set; }

            public static bool Validate(Configuration configuration)
            {
                return configuration.Width > 0
                    && configuration.Height > 0
                    && configuration.WinSequenceLength > 1
                    && configuration.WinSequenceLength <= configuration.Width
                    && configuration.WinSequenceLength <= configuration.Height;
            }
        }
    }
}
