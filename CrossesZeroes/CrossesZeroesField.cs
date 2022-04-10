using System;

namespace CrossesZeroes
{
    public class CrossesZeroesField
    {
        protected CellState[,] field = new CellState[3, 3];

        public CrossesZeroesField()
        {
            @readonly = new(() => new(this));
        }

        public virtual CellState this[int i, int j]
        {
            get => field[i, j];
        }

        public virtual int Size { get; } = 3;

        public virtual void Set(Point point, CellState markType)
        {
            if (markType == CellState.Empty) throw new ArgumentException("", nameof(markType));
            if (field[point.x, point.y] != CellState.Empty) throw new ArgumentException("", nameof(point));

            field[point.x, point.y] = markType;
        }

        private readonly Point[][] winIndexes = new Point[][]
        {
            new Point[]
            {
                new(0,0),
                new(0,1),
                new(0,2)
            },
            new Point[]
            {
                new(1,0),
                new(1,1),
                new(1,2)
            },
            new Point[]
            {
                new(2,0),
                new(2,1),
                new(2,2)
            },

            new Point[]
            {
                new(0,0),
                new(1,0),
                new(2,0)
            },
            new Point[]
            {
                new(0,1),
                new(1,1),
                new(2,1)
            },
            new Point[]
            {
                new(0,1),
                new(1,1),
                new(2,1)
            },

            new Point[]
            {
                new(0,0),
                new(1,1),
                new(2,2)
            },
            new Point[]
            {
                new(2,0),
                new(1,1),
                new(0,2)
            }
};
        public virtual bool IsWin(out CellState winner)
        {
            foreach (var seq in winIndexes)
            {
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
                    winner = CellState.Cross;
                    return true;
                }

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
                    winner = CellState.Zero;
                    return true;
                }
            }

            winner = CellState.Empty;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (field[i, j] == CellState.Empty) return false;
            return true;
        }

        public virtual void Clear()
        {
            field = new CellState[3, 3];
        }

        private readonly Lazy<ReadonlyField> @readonly;

        public virtual CrossesZeroesField AsReadonly() => @readonly.Value;
    }
}