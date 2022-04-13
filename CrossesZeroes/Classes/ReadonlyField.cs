using System;

namespace CrossesZeroes
{
    /// <summary>
    /// Поле игры в крестики-нолики только для чтения
    /// </summary>
    public sealed class ReadonlyField : CrossesZeroesField
    {
        private readonly CrossesZeroesField proxied;

        /// <summary>
        /// Создаёт новый экземпляр
        /// </summary>
        /// <param name="proxied">Замещаемый экземпляр</param>
        public ReadonlyField(CrossesZeroesField proxied)
        {
            this.proxied = proxied;
        }

        public override CellState this[int i, int j] => proxied[i, j];

        public override int Size => proxied.Size;

        public override void Set(Point point, CellState markType)
        {
            throw new InvalidOperationException();
        }

        public override CrossesZeroesField AsReadonly() => this;
    }
}