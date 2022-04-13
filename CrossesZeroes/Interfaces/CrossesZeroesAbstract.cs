using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    /// <summary>
    /// Базовый класс для всех реализаций игры крестики-нолики
    /// </summary>
    abstract class CrossesZeroesAbstract
    {
        //Игроки
        protected IPlayer cross;
        protected IPlayer zero;
        //Поле
        protected readonly CrossesZeroesField field;

        /// <summary>
        /// Конструктор, через который внедряются зависимости класса
        /// </summary>
        /// <param name="player1">Первый игрок</param>
        /// <param name="player2">Второй игрок</param>
        /// <param name="field">Поле</param>
        public CrossesZeroesAbstract(IPlayer player1, IPlayer player2, CrossesZeroesField field)
        {
            cross = player1;
            zero = player2;
            this.field = field;
        }

        /// <summary>
        /// Метод, обнуляющий текуще состояние игры, начиная её сначала
        /// </summary>
        public abstract void Restart();

        /// <summary>
        /// Метод хода в игре. Позволяет сделать ход каждому из участников
        /// </summary>
        /// <returns>Возможны ли дальнейшие ходы</returns>
        public abstract bool Turn();
    }
}
