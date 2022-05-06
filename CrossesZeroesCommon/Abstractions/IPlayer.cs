using CrossesZeroes.Common;

namespace CrossesZeroes.Abstractions
{
    /// <summary>
    /// Интерфейс игрока
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Метод хода для игрока
        /// </summary>
        /// <param name="field">Текущее состояние поля</param>
        /// <returns>Точка, которая должна быть отмечена в результате хода</returns>
        Point Turn(ICrossesZeroesField field);

        /// <summary>
        /// Метод, сообщающий игроку о завершении игры
        /// </summary>
        /// <param name="victory">Победил ли данный игрок</param>
        void ReportEnd(bool victory);

        /// <summary>
        /// Ставит знак хода для игрока (крестик или нолик)
        /// </summary>
        /// <param name="mark">Вид знака</param>
        void SetMark(CellState mark);
    }
}