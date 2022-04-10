using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    abstract class CrossesZeroesAbstract
    {
        protected IPlayer cross;
        protected IPlayer zero;
        protected readonly CrossesZeroesField field;

        public CrossesZeroesAbstract(IPlayer player1, IPlayer player2, CrossesZeroesField field)
        {
            cross = player1;
            zero = player2;
            this.field = field;
        }

        public abstract void Restart();

        public abstract bool Turn();
    }
}
