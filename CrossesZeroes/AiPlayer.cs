using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    class AiPlayer : IAiPlayer
    {
        public void ReportEnd(bool victory)
        { }

        public void SetMark(CellState mark)
        {}

        public Point Turn(CrossesZeroesField field)
        {
            for (int i = 0; i < field.Size; i++)
                for (int j = 0; j < 3; j++)
                    if (field[i, j] == CellState.Empty) return new(i, j);

            throw new InvalidProgramException();
        }
    }
}
