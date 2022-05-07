using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using System;

namespace CrossesZeroes
{
    /// <summary>
    /// Поле игры в крестики-нолики только для чтения
    /// </summary>
    public sealed class ReadonlyField : ICrossesZeroesField
    {
        private readonly ICrossesZeroesField proxied;

        /// <summary>
        /// Создаёт новый экземпляр
        /// </summary>
        /// <param name="proxied">Замещаемый экземпляр</param>
        public ReadonlyField(ICrossesZeroesField proxied)
        {
            this.proxied = proxied;
        }

        public CellState this[int i, int j] => proxied[i, j];

        public int Height => proxied.Height;
        public int Width => proxied.Width;

        public void Set(Point point, CellState markType)
        {
            throw new InvalidOperationException();
        }

        public ICrossesZeroesField AsReadonly() => this;

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public bool IsEndGame(out CellState winner) => proxied.IsEndGame(out winner);
    }
}