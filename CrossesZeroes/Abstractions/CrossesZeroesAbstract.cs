using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Abstractions
{
    /// <summary>
    /// Базовый класс для всех реализаций игры крестики-нолики
    /// </summary>
    public abstract class CrossesZeroesAbstract : ICrossesZeroesGame
    {
        //Игроки
        protected IPlayer cross;
        protected IPlayer zero;
        //Поле
        protected readonly ICrossesZeroesField field;
        protected readonly ILogger<CrossesZeroesAbstract> logger;

        /// <summary>
        /// Конструктор, через который внедряются зависимости класса
        /// </summary>
        /// <param name="player1">Первый игрок</param>
        /// <param name="player2">Второй игрок</param>
        /// <param name="field">Поле</param>
        public CrossesZeroesAbstract(IPlayer player1, IPlayer player2, ICrossesZeroesField field, ILogger<CrossesZeroesAbstract> logger)
        {
            cross = player1;
            zero = player2;
            this.field = field;
            this.logger = logger;
        }

        /// <summary>
        /// Метод, обнуляющий текуще состояние игры, начиная её сначала
        /// </summary>
        public abstract void Restart();

        /// <summary>
        /// Метод хода в игре. Позволяет сделать ход каждому из участников
        /// </summary>
        /// <returns>Возможны ли дальнейшие ходы</returns>
        public abstract Task<bool> Turn();

        public virtual async Task<bool> IsRestartWanted()
        {
            Task<bool> crossTask = cross.IsRepeatWanted();
            Task<bool> zeroTask = zero.IsRepeatWanted();
            return await zeroTask & await crossTask;
        }
    }
}
