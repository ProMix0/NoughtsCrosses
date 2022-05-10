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

        public CrossesZeroesLoopService(CrossesZeroesAbstract game)
        {
            this.game = game;
        }

        protected async override Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (!token.IsCancellationRequested && await game.Turn())
                    token.ThrowIfCancellationRequested();

                if (!await game.IsRestartWanted()) break;

                game.Restart();
            }
        }
    }
}
