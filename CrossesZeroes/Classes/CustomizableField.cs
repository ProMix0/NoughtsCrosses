using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using CrossesZeroes.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossesZeroes.Classes
{
    public class CustomizableField : CrossesZeroesField
    {

        public CustomizableField(IOptions<Configuration> config, ILogger<CustomizableField> logger, ReadonlyFieldBinder readonlyBinder)
            : this(config.Value.Height, config.Value.Width, config.Value.WinSequenceLength, logger, readonlyBinder)
        { }

        private CustomizableField(int height, int width, int winSequenceLength, ILogger<CustomizableField> logger, ReadonlyFieldBinder readonlyBinder)
            :base(logger,readonlyBinder)
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
                if (value <= 0) logger.LogMessageAndThrow(new ArgumentException("Must be more than zero", nameof(Width)));
                width = value;
            }
        }
        private int width;

        public override int Height
        {
            get => height;
            protected set
            {
                if (value <= 0) logger.LogMessageAndThrow(new ArgumentException("Must be more than zero", nameof(Height)));
                height = value;
            }
        }
        private int height;

        protected int WinSequenceLength
        {
            get => winSequenceLength; set
            {
                if (value <= 0) logger.LogMessageAndThrow(new ArgumentException("Must be more than zero", nameof(WinSequenceLength)));
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
            for (int i = 0; i <= field.GetLength(0) - winSequenceLength; i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    GetCol(i, j);

            void GetCol(int i, int j)
            {
                List<Point> temp = new();

                for (int k = 0; k < winSequenceLength; k++)
                    temp.Add(new(i + k, j));

                result.Add(temp);
            }

            //Горизонтальные
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j <= field.GetLength(1) - winSequenceLength; j++)
                    GetRow(i, j);

            void GetRow(int i, int j)
            {
                List<Point> temp = new();

                for (int k = 0; k < winSequenceLength; k++)
                    temp.Add(new(i, j + k));

                result.Add(temp);
            }

            //Диагональные возрастающие
            for (int i = winSequenceLength - 1; i < field.GetLength(0); i++)
                for (int j = 0; j <= field.GetLength(1) - winSequenceLength; j++)
                    GetUpDiagonal(i, j);

            void GetUpDiagonal(int i, int j)
            {
                List<Point> temp = new();

                for (int k = 0; k < winSequenceLength; k++)
                    temp.Add(new(i - k, j + k));

                result.Add(temp);
            }

            //Диагональные убывающие
            for (int i = 0; i <= field.GetLength(0) - winSequenceLength; i++)
                for (int j = 0; j <= field.GetLength(1) - winSequenceLength; j++)
                    GetDownDiagonal(i, j);

            void GetDownDiagonal(int i, int j)
            {
                List<Point> temp = new();
                for (int k = 0; k < winSequenceLength; k++)
                    temp.Add(new(i + k, j + k));
                result.Add(temp);
            }

            return result;
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
