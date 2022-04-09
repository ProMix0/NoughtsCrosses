using System;

namespace SeaBattle
{
    [Flags]
    public enum FieldCellState:byte
    {
        Empty=0,
        Ship=1,
        EmptyFired=2,
        ShipFired=3
    }
}