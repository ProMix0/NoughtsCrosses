using CrossesZeroes.Classes;
using CrossesZeroes.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using AutoFixture.Xunit2;
using CrossesZeroes.Tests.TestClasses;

namespace CrossesZeroes.Tests
{
    public class CrossesZeroesFieldTests
    {
        [Theory, AutoMoqData]
        public void Set_ShouldThrowOnEmptyCellState(CrossesZeroesField field)
        {
            //CrossesZeroesField field = new(NullLogger<CrossesZeroesField>.Instance);

            field.Invoking(field => field.Set(new(), CellState.Empty)).Should().Throw<ArgumentException>();
        }

        [Theory, AutoMoqData]
        public void Set_ShouldThrowOnFilledPoint(CrossesZeroesField field)
        {
            //CrossesZeroesField field = new(NullLogger<CrossesZeroesField>.Instance);

            field.Set(new(0, 0), CellState.Cross);

            field.Invoking(field => field.Set(new(0, 0), CellState.Cross)).Should().Throw<ArgumentException>();
        }
    }
}
