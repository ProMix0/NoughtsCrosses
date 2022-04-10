using System;

namespace CrossesZeroes
{
    public sealed class ReadonlyField : CrossesZeroesField
    {
        private readonly CrossesZeroesField proxied;

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