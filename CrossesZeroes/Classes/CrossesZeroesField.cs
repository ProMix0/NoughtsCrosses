using System;

namespace CrossesZeroes
{
    /// <summary>
    /// Поле для крестиков-ноликов
    /// </summary>
    public class CrossesZeroesField
    {
        //Матрица клеток
        protected CellState[,] field = new CellState[3, 3];

        public CrossesZeroesField()
        {
            @readonly = new(() => new(this));
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

        /// <summary>
        /// Размер поля (квадрат)
        /// </summary>
        public virtual int Size { get; } = 3;

        /// <summary>
        /// Устанавливает состояние клетки поля
        /// </summary>
        /// <param name="point">Координаты клетки</param>
        /// <param name="markType">Состояние</param>
        public virtual void Set(Point point, CellState markType)
        {
            if (markType == CellState.Empty) throw new ArgumentException("", nameof(markType));
            if (field[point.x, point.y] != CellState.Empty) throw new ArgumentException("", nameof(point));

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
            //Перебор всех победных последовательностей
            foreach (var seq in winIndexes)
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
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    //Если хоть одна клетка пустая, то игра не завершена
                    if (field[i, j] == CellState.Empty) return false;
            //Иначе ничья
            return true;
        }

        /// <summary>
        /// Очистка поля
        /// </summary>
        public virtual void Clear()
        {
            field = new CellState[3, 3];
        }

        private readonly Lazy<ReadonlyField> @readonly;

        /// <summary>
        /// Возвращает объект только для чтения
        /// </summary>
        /// <returns>Объект только для чтения</returns>
        public virtual CrossesZeroesField AsReadonly() => @readonly.Value;
    }
}