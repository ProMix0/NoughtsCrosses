using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Classes
{
    class CustomizableField : ICrossesZeroesField
    {

        public CustomizableField(IOptions<Configuration> config)
            : this(config.Value.Height, config.Value.Width, config.Value.WinSequenceLength)
        { }

        public CustomizableField(int height, int width, int winSequenceLength)
        {
            Width = width > 0 ? width : throw new ArgumentException("Must be more than zero", nameof(width));
            Height = height > 0 ? height : throw new ArgumentException("Must be more than zero", nameof(height));
            this.winSequenceLength = winSequenceLength > 0 ? winSequenceLength : throw new ArgumentException("Must be more than zero", nameof(winSequenceLength));

            field = new CellState[height, width];
            readonlyField = new(this);
        }

        public CellState this[int i, int j] => field[i, j];
        private CellState[,] field;

        public int Width { get; }

        public int Height { get; }

        protected int winSequenceLength;

        private readonly ReadonlyField readonlyField;
        public ICrossesZeroesField AsReadonly() => readonlyField;

        public virtual void Clear()
        {
            field = new CellState[Height, Width];
        }

        public virtual bool IsEndGame(out CellState winner)
        {
            //Перебор всех победных последовательностей
            foreach (var seq in WinIndexes())
            {
                //Если все крестики
                bool crosses = true;
                foreach (var point in seq)
                {
                    if (field[point.x, point.y] != CellState.Cross)
                    {
                        crosses = false;
                        break;
                    }
                }
                if (crosses)
                {
                    //...то крестики победили
                    winner = CellState.Cross;
                    return true;
                }

                //Если все нолики
                bool zeroes = true;
                foreach (var point in seq)
                {
                    if (field[point.x, point.y] != CellState.Zero)
                    {
                        zeroes = false;
                        break;
                    }
                }
                if (zeroes)
                {
                    //...то нолики победили
                    winner = CellState.Zero;
                    return true;
                }
            }


            winner = CellState.Empty;
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    //Если хоть одна клетка пустая, то игра не завершена
                    if (field[i, j] == CellState.Empty) return false;
            //Иначе ничья
            return true;
        }

        protected virtual IEnumerable<IEnumerable<Point>> WinIndexes()
        {
            //Вертикальные
            for (int i = 0; i <= field.GetLength(0) - winSequenceLength; i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    yield return GetCol(i, j);

            IEnumerable<Point> GetCol(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i + k, j);
            }

            //Горизонтальные
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j <= field.GetLength(1) - winSequenceLength; j++)
                    yield return GetRow(i, j);

            IEnumerable<Point> GetRow(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i, j + k);
            }

            //Диагональные возрастающие
            for (int i = winSequenceLength - 1; i < field.GetLength(0); i++)
                for (int j = 0; j <= field.GetLength(1) - winSequenceLength; j++)
                    yield return GetUpDiagonal(i, j);

            IEnumerable<Point> GetUpDiagonal(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i - k, j + k);
            }

            //Диагональные убывающие
            for (int i = 0; i <= field.GetLength(0) - winSequenceLength; i++)
                for (int j = 0; j <= field.GetLength(1) - winSequenceLength; j++)
                    yield return GetDownDiagonal(i, j);

            IEnumerable<Point> GetDownDiagonal(int i, int j)
            {
                for (int k = 0; k < winSequenceLength; k++)
                    yield return new(i + k, j + k);
            }
        }

        public virtual void Set(Point point, CellState markType)
        {
            if (markType == CellState.Empty) throw new ArgumentException("", nameof(markType));
            if (field[point.x, point.y] != CellState.Empty) throw new ArgumentException("", nameof(point));

            field[point.x, point.y] = markType;
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
