using CrossesZeroes.Classes;
using CrossesZeroes.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Tests.TestClasses
{
    internal class TestField : CrossesZeroesField
    {
        public TestField() : base(NullLogger<CrossesZeroesField>.Instance)
        {

        }

        public new CellState this[int i, int j]
        {
            get => base[i, j];
            set => field[i, j] = value;
        }
    }
}
