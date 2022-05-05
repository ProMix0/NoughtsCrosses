using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrossesZeroes.Services
{
    internal class CrossesZeroesLoopService : BackgroundService
    {
        private readonly CrossesZeroesAbstract game;

        public CrossesZeroesLoopService(CrossesZeroesAbstract game)
        {
            this.game = game;
        }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            return Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    while (!token.IsCancellationRequested && game.Turn()) ;

                    Console.WriteLine("Restart? (Y/N)");
                    if (Console.ReadKey().Key != ConsoleKey.Y) break;

                    game.Restart();
                }
            }, token);
        }
    }
}
