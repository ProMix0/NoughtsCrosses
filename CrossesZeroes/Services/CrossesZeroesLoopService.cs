using CrossesZeroes.Abstractions;
﻿using BetterHostedServices;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrossesZeroes.Services
{
    public class CrossesZeroesLoopService : CriticalBackgroundService
    {
        private readonly CrossesZeroesAbstract game;
        private readonly IHostLifetime lifetime;

        public CrossesZeroesLoopService(CrossesZeroesAbstract game, IHostApplicationLifetime lifetime, ILogger<CrossesZeroesLoopService> logger)
            : base(null)
        {
            this.game = game;
            this.lifetime = lifetime;
            this.logger = logger;
        }

        protected override void OnError(Exception exceptionFromExecuteAsync)
        {
            logger.LogMessageAndThrow(exceptionFromExecuteAsync);
        }

        protected async override Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (!token.IsCancellationRequested && await game.Turn())
                    token.ThrowIfCancellationRequested();

                bool restart = await game.IsRestartWanted();
                if (!restart) break;

                await lifetime.StopAsync(token);
                game.Restart();
            }

        }
    }
}
