using CrossesZeroes.Abstractions;
using CrossesZeroes.Classes;
using CrossesZeroes.Common;
using CrossesZeroes.Tests.TestClasses;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CrossesZeroes.Tests
{
    public class AiPlayerTests
    {
        private static readonly TestField field = new ();

        private void FillField()
        {
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    field[i, j] = CellState.Cross;
        }

        public static IEnumerable<object[]> IterateIndexes()
        {
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    yield return new object[] { i, j };
        }

        [Theory]
        [MemberData(nameof(IterateIndexes))]
        public void Turn_ShouldFillEmpty(int i,int j)
        {
            FillField();
            field[i, j] = CellState.Empty;

            AiPlayer player = new(Options.Create<AiPlayer.AiPlayerBehaviour>(new()), NullLogger<AiPlayer>.Instance);
            player.Init(CellState.Cross, field);

            player.Turn().Result.Should().Be(new Point(i, j));
        }

        [Fact]
        public void Turn_ShouldThrowFilled()
        {
            FillField();

            AiPlayer player = new(Options.Create<AiPlayer.AiPlayerBehaviour>(new()), NullLogger<AiPlayer>.Instance);
            player.Init(CellState.Cross, field);

            player.Invoking(pl => pl.Turn()).Should().ThrowAsync<InvalidOperationException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RepeatWanted_ShouldReturnAsParam(bool behavior)
        {
            AiPlayer player = new(Options.Create<AiPlayer.AiPlayerBehaviour>(new() { wantRepeat=behavior}), NullLogger<AiPlayer>.Instance);
            player.IsRepeatWanted().Should().Be(Task.FromResult(behavior));
        }
    }
}