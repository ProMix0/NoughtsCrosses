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
        private ICrossesZeroesField field;

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

        public void ReportEnd(bool victory)
        { }

        public void Init(CellState mark, ICrossesZeroesField field)
        {
            this.field = field;
        }

        public Task<Point> Turn()
        {
            //Перебор всех клеток в поиске свободных
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (field[i, j] == CellState.Empty) return Task.FromResult(new Point(i, j));

            throw new InvalidProgramException();
        }

        public Task<bool> IsRepeatWanted() =>
            Task.FromResult(behaviour.wantRepeat);

        public void NotifyFieldChange(Point point)
        {
            //throw new NotImplementedException();
        }

        public class AiPlayerBehaviour
        {
            public bool wantRepeat;
        }
    }
}
