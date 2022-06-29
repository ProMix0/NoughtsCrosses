using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Base noughts and crosses field
    /// </summary>
    public class GameField : IGameField
    {
        protected readonly ILogger<GameField> logger;

        protected readonly ReadonlyField readonlyField;

        // List of win sequences to iterate (dumb solution)
        private readonly Point[][] winIndexes = new Point[][]
        {
            new Point[]
            {
                new(0, 0),
                new(0, 1),
                new(0, 2)
            },
            new Point[]
            {
                new(1, 0),
                new(1, 1),
                new(1, 2)
            },
            new Point[]
            {
                new(2, 0),
                new(2, 1),
                new(2, 2)
            },

            new Point[]
            {
                new(0, 0),
                new(1, 0),
                new(2, 0)
            },
            new Point[]
            {
                new(0, 1),
                new(1, 1),
                new(2, 1)
            },
            new Point[]
            {
                new(0, 2),
                new(1, 2),
                new(2, 2)
            },

            new Point[]
            {
                new(0, 0),
                new(1, 1),
                new(2, 2)
            },
            new Point[]
            {
                new(2, 0),
                new(1, 1),
                new(0, 2)
            }
        };

        protected CellState[,] field = new CellState[3, 3];

        public GameField(ILogger<GameField> logger)
        {
            readonlyField = new(this, logger);
            this.logger = logger;
        }

        public virtual CellState this[int i, int j]
        {
            get => field[i, j];
        }

        public virtual int Width { get; protected set; } = 3;
        public virtual int Height { get; protected set; } = 3;

        public virtual void Set(Point point, CellState markType)
        {
            if (markType == CellState.Empty)
                throw logger.LogExceptionMessage(new ArgumentException("Unknown cell state", nameof(markType)));

            if (field[point.X, point.Y] != CellState.Empty)
                throw logger.LogExceptionMessage(new ArgumentException("Cell already filled", nameof(point)));

            field[point.X, point.Y] = markType;
        }

        public virtual bool IsEndGame(out CellState winner)
        {
            winner = CellState.Empty;

            foreach (var seq in WinIndexes())
            {
                // All crosses
                if (CheckWin(seq, CellState.Cross, ref winner))
                    return true;

                // All noughts
                if (CheckWin(seq, CellState.Zero, ref winner))
                    return true;
            }

            return CheckEndGame();

            bool CheckWin(IEnumerable<Point> seq, CellState player, ref CellState winner)
            {
                bool win = true;
                foreach (var point in seq)
                {
                    if (field[point.X, point.Y] != player)
                    {
                        win = false;
                        break;
                    }
                }

                if (win)
                {
                    winner = player;
                }

                return win;
            }

            bool CheckEndGame()
            {
                for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    // If empty cells exist - game not ended
                    if (field[i, j] == CellState.Empty)
                        return false;
                // Otherwise draw
                return true;
            }
        }

        public virtual void Clear()
        {
            field = new CellState[3, 3];
        }

        public virtual ReadonlyField AsReadonly() => readonlyField;

        /// <summary>
        /// Internal win indexes enumeration helper
        /// </summary>
        /// <returns>List of win sequences</returns>
        protected virtual IEnumerable<IEnumerable<Point>> WinIndexes() => winIndexes;
    }
}