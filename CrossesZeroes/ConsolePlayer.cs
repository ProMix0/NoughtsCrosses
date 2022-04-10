using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes
{
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

        public Point Turn(CrossesZeroesField field)
        {
            Console.Clear();

            Console.WriteLine(mark == CellState.Cross ? "You are cross!" : "You are zero!");

            for(int i = 0; i < field.Size; i++)
            {
                for (int j = 0; j < field.Size; j++)
                    Console.Write(field[i, j] switch
                    {
                        CellState.Empty => ' ',
                        CellState.Cross => 'X',
                        CellState.Zero => '0',
                        _ => '?'
                    });
                Console.WriteLine();
            }

            Console.WriteLine($"Enter turn coords (zero-based)\nMust be from 0 to {field.Size-1}\nAlso must be empty");
            while (true)
            {
                string[] strInput = Console.ReadLine().Split();
                if (strInput.Length >= 2)
                {
                    int[] input = strInput.Take(2).Select(int.Parse).ToArray();
                    if (input[0] >= 0 && input[0] < field.Size)
                        if (input[1] >= 0 && input[1] < field.Size)
                            if (field[input[0], input[1]] == CellState.Empty)
                                return new(input[0], input[1]);
                }
                Console.CursorTop--;
                Console.CursorLeft = 0;
                Console.Write(new string(' ', Console.BufferWidth));
                Console.CursorLeft = 0;
            }
        }
    }
}
