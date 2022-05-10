﻿using CrossesZeroes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Игра в крестики-нолики человека с компьютером. Используется только в DI
    /// </summary>
    public class CrossesZeroesWithAi : CrossesZeroesGame
    {
        public CrossesZeroesWithAi(IRealPlayer player1, IAiPlayer player2, ICrossesZeroesField field)
            : base(player1, player2, field)
        { }
    }
}
