using BetterHostedServices;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NoughtsCrosses.Services
{
    public class GameLoopService : CriticalBackgroundService
    {
        private readonly AbstractGame game;
        private readonly IHostApplicationLifetime lifetime;
        private readonly ILogger<GameLoopService> logger;

        public GameLoopService(AbstractGame game, IHostApplicationLifetime lifetime, ILogger<GameLoopService> logger)
            : base(null)
        {
            this.game = game;
            this.lifetime = lifetime;
            this.logger = logger;
        }

        protected override void OnError(Exception exceptionFromExecuteAsync)
        {
            throw logger.LogExceptionMessage(exceptionFromExecuteAsync);
        }

        protected async override Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (!token.IsCancellationRequested && await game.Turn())
                    token.ThrowIfCancellationRequested();

                bool restart = await game.IsRestartWanted();
                if (!restart) break;

                game.Restart();
            }

            lifetime.StopApplication();
        }
    }
}
