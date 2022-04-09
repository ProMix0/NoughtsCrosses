using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    class ConsoleBattlePlayer : IBattlePlayer
    {
        private ArrowKeyListener keyListener;

        public ConsoleBattlePlayer(ArrowKeyListener keyListener)
        {
            this.keyListener = keyListener;
        }

        public void ReportEnd(bool victory)
        {
            Console.WriteLine(victory ? "You win!" : "Defeated...");
        }

        public void SetShips(IEnumerable<Ship> ships, SeaBattleField field)
        {
            //TODO user ships positions
            Random random = new();
            foreach(var ship in ships)
            {
                field.AddShip(ship, new Point(random.Next(field.Size), random.Next(field.Size)));
            }
        }

        //Empty _
        //Empty fired X
        //Ship O
        //Ship fired #
        public Point Turn(SeaBattleField ally, SeaBattleField enemy)
        {
            Console.Clear();
            PrintField(ally);
            Console.WriteLine(new string('-', Console.BufferWidth));
            PrintField(enemy);

            Point point;
            while(true)
            {
                int[] input = Console.ReadLine().Split().Select(int.Parse).Take(2).ToArray();
                if (input[0] is >= 0 && input[0] < ally.Size)
                    if (input[1] is >= 0 && input[1] < ally.Size)
                    {
                        point = new(input[0], input[1]);
                        break;
                    }
                Console.CursorTop--;
                Console.Write(new string(' ', Console.BufferWidth));
                Console.CursorLeft = 0;
            }

            return point;

            static void PrintField(SeaBattleField ally)
            {
                for (int i = 0; i < ally.Size; i++)
                {
                    for (int j = 0; j < ally.Size; j++)
                    {
                        Console.Write(ally.Field[i, j] switch
                        {
                            FieldCellState.Empty => '_',
                            FieldCellState.EmptyFired => 'X',
                            FieldCellState.Ship => 'O',
                            FieldCellState.ShipFired => '#',
                            _ => '@'
                        });
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
