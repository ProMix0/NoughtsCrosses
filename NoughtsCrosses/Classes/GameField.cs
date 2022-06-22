﻿using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using NoughtsCrosses.Utils;
using Microsoft.Extensions.Logging;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Поле для крестиков-ноликов
    /// </summary>
    public class GameField : IGameField
    {
        //Матрица клеток
        protected CellState[,] field = new CellState[3, 3];

        public GameField(ILogger<GameField> logger)
        {
            readonlyField = new(this,logger);
            this.logger = logger;
        }

        /// <summary>
        /// Клетка на поле
        /// </summary>
        /// <param name="i">Строка</param>
        /// <param name="j">Столбец</param>
        /// <returns>Состояние клетки</returns>
        public virtual CellState this[int i, int j]
        {
            get => field[i, j];
        }

        public virtual int Width { get; protected set; } = 3;
        public virtual int Height { get; protected set; } = 3;

        /// <summary>
        /// Устанавливает состояние клетки поля
        /// </summary>
        /// <param name="point">Координаты клетки</param>
        /// <param name="markType">Состояние</param>
        public virtual void Set(Point point, CellState markType)
        {
            if (markType == CellState.Empty)
                throw logger.LogExceptionMessage(new ArgumentException("Unknown cell state", nameof(markType)));

            if (field[point.x, point.y] != CellState.Empty)
                throw logger.LogExceptionMessage(new ArgumentException("Cell already filled", nameof(point)));

            field[point.x, point.y] = markType;
        }

        //Список последовательней, которые надо проверять для проверки выигрыша на поле 3x3 (ленивое решение)
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
                new(0,2),
                new(1,2),
                new(2,2)
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

        protected virtual IEnumerable<IEnumerable<Point>> WinIndexes() => winIndexes;

        /// <summary>
        /// Проверка на окончание игры
        /// </summary>
        /// <param name="winner">Параметр, возвращающий победителя<para/>
        /// <see cref="CellState.Cross"/> - крестики выиграли<br/>
        /// <see cref="CellState.Zero"/> - нолики выиграли<br/>
        /// <see cref="CellState.Empty"/> - ничья выиграли</param>
        /// <returns></returns>
        public virtual bool IsEndGame(out CellState winner)
        {
            winner = CellState.Empty;

            foreach (var seq in WinIndexes())
            {
                //Если все крестики
                if (CheckWin(seq, CellState.Cross, ref winner))
                    return true;

                //Если все нолики
                if (CheckWin(seq, CellState.Zero, ref winner))
                    return true;
            }

            return CheckEndGame();

            bool CheckWin(IEnumerable<Point> seq, CellState player, ref CellState winner)
            {
                bool win = true;
                foreach (var point in seq)
                {
                    if (field[point.x, point.y] != player)
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
                        //Если хоть одна клетка пустая, то игра не завершена
                        if (field[i, j] == CellState.Empty) return false;
                //Иначе ничья
                return true;
            }
        }

        /// <summary>
        /// Очистка поля
        /// </summary>
        public virtual void Clear()
        {
            field = new CellState[3, 3];
        }

        protected readonly ReadonlyField readonlyField;
        protected readonly ILogger<GameField> logger;

        /// <summary>
        /// Возвращает объект только для чтения
        /// </summary>
        /// <returns>Объект только для чтения</returns>
        public virtual ReadonlyField AsReadonly() => readonlyField;
    }
}