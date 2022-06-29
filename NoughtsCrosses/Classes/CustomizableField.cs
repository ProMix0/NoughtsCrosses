using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NoughtsCrosses.Common;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// The field allow to customize some parameters like field size and win sequence length
    /// </summary>
    public class CustomizableField : GameField
    {
        private int height;
        private int width;


        private List<List<Point>>? winIndexesCache = null;
        private int winSequenceLength;

        public CustomizableField(IOptions<Configuration> config, ILogger<CustomizableField> logger)
            : this(config.Value.Height, config.Value.Width, config.Value.WinSequenceLength, logger)
        {
        }

        private CustomizableField(int height, int width, int winSequenceLength, ILogger<CustomizableField> logger)
            : base(logger)
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
                if (value <= 0)
                    throw logger.LogExceptionMessage(new ArgumentException("Must be more than zero", nameof(Width)));
                width = value;
            }
        }

        public override int Height
        {
            get => height;
            protected set
            {
                if (value <= 0)
                    throw logger.LogExceptionMessage(new ArgumentException("Must be more than zero", nameof(Height)));
                height = value;
            }
        }

        protected int WinSequenceLength
        {
            get => winSequenceLength;
            set
            {
                if (value <= 0)
                    throw logger.LogExceptionMessage(new ArgumentException("Must be more than zero",
                        nameof(WinSequenceLength)));
                winSequenceLength = value;
            }
        }

        public override void Clear()
        {
            field = new CellState[Height, Width];
        }

        protected override IEnumerable<IEnumerable<Point>> WinIndexes() => winIndexesCache ??= GenerateWinIndexes();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return list of sequences (that also list)</returns>
        private List<List<Point>> GenerateWinIndexes()
        {
            List<List<Point>> result = new();

            AddVerticalIndexes();

            AddHorizontalIndexes();

            AddDiagonalUpIndexes();

            AddDiagonalDownIndexes();

            return result;


            void AddVerticalIndexes()
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

            void AddHorizontalIndexes()
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

            void AddDiagonalUpIndexes()
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

            void AddDiagonalDownIndexes()
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

        /// <summary>
        /// Options class for <see cref="CustomizableField"/>
        /// </summary>
        public class Configuration
        {
            /// <summary>
            /// Default section name in config
            /// </summary>
            public static readonly string SectionName = nameof(CustomizableField);

            /// <summary>
            /// Count of columns
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// Count of rows
            /// </summary>
            public int Height { get; set; }

            /// <summary>
            /// Count of marks in a row to win
            /// </summary>
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