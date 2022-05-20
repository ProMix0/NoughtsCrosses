using CrossesZeroes.Common;

namespace CrossesZeroes.Abstractions
{
    /// <summary>
    /// Интерфейс игрока
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Ставит знак хода для игрока (крестик или нолик)
        /// </summary>
        /// <param name="mark">Вид знака</param>
        void Init(CellState mark, ICrossesZeroesField field);

        void NotifyFieldChange(Point point);

        /// <summary>
        /// Метод хода для игрока
        /// </summary>
        /// <param name="field">Текущее состояние поля</param>
        /// <returns>Точка, которая должна быть отмечена в результате хода</returns>
        Task<Point> Turn();

        /// <summary>
        /// Метод, сообщающий игроку о завершении игры
        /// </summary>
        /// <param name="victory">Победил ли данный игрок</param>
        void ReportEnd(bool victory);

        Task<bool> IsRepeatWanted();
    }
}