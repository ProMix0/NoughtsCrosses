using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;

namespace NoughtsCrosses.Classes
{

    public class ConsolePlayer : IPlayer
    {
        private IGameField field = null!;

        private CellState mark;

        public void ReportEnd(bool victory)
        {
            PrintField(header: victory ? "You win!" : "You lose...");
        }

        public void Init(CellState mark, IGameField field)
        {
            this.mark = mark;
            this.field = field;
        }

        public Task<Point> Turn()
        {
            PrintField(mark == CellState.Cross ? "You are cross!" : "You are zero!");

            Console.WriteLine(
                $"Enter turn coords (zero-based)\nMust be from 0 to {field.Height - 1} and from 0 to {field.Width - 1}\nAlso must be empty");
            while (true)
            {
                string[] strInput = Console.ReadLine()!.Split();
                if (strInput.Length >= 2)
                {
                    int[] input = strInput.Take(2).Select(int.Parse).ToArray();
                    if (input[0] >= 0 && input[0] < field.Height)
                        if (input[1] >= 0 && input[1] < field.Width)
                            if (field[input[0], input[1]] == CellState.Empty)
                                return Task.FromResult(new Point(input[0], input[1]));
                }

                Console.CursorTop--;
                Console.CursorLeft = 0;
                Console.Write(new string(' ', Console.BufferWidth - 1));
                Console.CursorLeft = 0;
            }
        }

        public Task<bool> IsRepeatWanted()
        {
            Console.WriteLine("Restart? (Y/N)");
            return Task.FromResult(Console.ReadKey().Key == ConsoleKey.Y);
        }

        public void NotifyFieldChange(Point point)
        {
            PrintField();
        }

        private void PrintField(string header = "")
        {
            Console.Clear();

            Console.WriteLine(header);

            Console.WriteLine($"+{new string('-', field.Width)}+");

            for (int i = 0; i < field.Height; i++)
            {
                Console.Write('|');
                for (int j = 0; j < field.Width; j++)
                    Console.Write(field[i, j] switch
                    {
                        CellState.Empty => ' ',
                        CellState.Cross => 'X',
                        CellState.Zero => '0',
                        _ => '?'
                    });
                Console.WriteLine('|');
            }

            Console.WriteLine($"+{new string('-', field.Width)}+");
        }
    }
}