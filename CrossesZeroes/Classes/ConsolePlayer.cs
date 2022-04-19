using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
    /// <summary>
    /// Представление физического игрока для вывода в консоль
    /// </summary>
    class ConsolePlayer : IRealPlayer
    {
        public void ReportEnd(bool victory)
        {
            Console.WriteLine(victory ? "You win!" : "You lose...");
        }

        private CellState mark;
        public void SetMark(CellState mark)
        {
            this.mark = mark;
        }

        public Point Turn(ICrossesZeroesField field)
        {
            Console.Clear();

            Console.WriteLine(mark == CellState.Cross ? "You are cross!" : "You are zero!");

            //Вывод поля в консоль
            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width; j++)
                    Console.Write(field[i, j] switch
                    {
                        CellState.Empty => ' ',
                        CellState.Cross => 'X',
                        CellState.Zero => '0',
                        _ => '?'
                    });
                Console.WriteLine();
            }

            Console.WriteLine($"Enter turn coords (zero-based)\nMust be from 0 to {field.Height - 1} and from 0 to {field.Width - 1}\nAlso must be empty");
            //Ожидание ввода корректных координат хода
            while (true)
            {
                string[] strInput = Console.ReadLine().Split();
                if (strInput.Length >= 2)
                {
                    int[] input = strInput.Take(2).Select(int.Parse).ToArray();
                    if (input[0] >= 0 && input[0] < field.Height)
                        if (input[1] >= 0 && input[1] < field.Width)
                            if (field[input[0], input[1]] == CellState.Empty)
                                return new(input[0], input[1]);
                }
                Console.CursorTop--;
                Console.CursorLeft = 0;
                Console.Write(new string(' ', Console.BufferWidth-1));
                Console.CursorLeft = 0;
            }
        }
    }
}
