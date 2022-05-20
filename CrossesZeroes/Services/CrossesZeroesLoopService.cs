using CrossesZeroes.Abstractions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrossesZeroes.Services
{
    public class CrossesZeroesLoopService : BackgroundService
    {
        private readonly CrossesZeroesAbstract game;
        private readonly IHostLifetime lifetime;

        public CrossesZeroesLoopService(CrossesZeroesAbstract game, IHostLifetime lifetime)
        {
            this.game = game;
            this.lifetime = lifetime;
        }

        protected async override Task ExecuteAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    while (!token.IsCancellationRequested && await game.Turn())
                        token.ThrowIfCancellationRequested();

                    bool restart = await game.IsRestartWanted();
                    if (!restart) break;

                    game.Restart();
                }

                await lifetime.StopAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine();

                Console.WriteLine(ex.Message);
                Console.WriteLine();

                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
