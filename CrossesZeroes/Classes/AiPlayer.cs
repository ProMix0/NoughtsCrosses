using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    /// <summary>
    /// Игрок-компьютер
    /// </summary>
    class AiPlayer : IAiPlayer
    {
        public void ReportEnd(bool victory)
        { }

        public void SetMark(CellState mark)
        {}

        public Point Turn(ICrossesZeroesField field)
        {
            //Перебор всех клеток в поиске свободных
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (field[i, j] == CellState.Empty) return new(i, j);

            throw new InvalidProgramException();
        }
    }
}
