using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using Microsoft.Extensions.Options;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Игрок-компьютер
    /// </summary>
    public class AiPlayer : IAiPlayer
    {
        private readonly AiPlayerBehaviour behaviour;

        public AiPlayer()
            : this(Options.Create(new AiPlayerBehaviour()
            {
                wantRepeat = true
            }))
        {

        }

        public AiPlayer(IOptions<AiPlayerBehaviour> behaviour)
        {
            this.behaviour = behaviour.Value;
        }

        public void ReportEnd(bool victory, ICrossesZeroesField field)
        { }

        public void Init(CellState mark)
        { }

        public Task<Point> Turn(ICrossesZeroesField field)
        {
            //Перебор всех клеток в поиске свободных
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (field[i, j] == CellState.Empty) return Task.FromResult(new Point(i, j));

            throw new InvalidProgramException();
        }

        public Task<bool> IsRepeatWanted() =>
            Task.FromResult(behaviour.wantRepeat);

        public class AiPlayerBehaviour
        {
            public bool wantRepeat;
        }
    }
}
