using BetterHostedServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.Services
{
    /// <summary>
    /// Service for simple run the game
    /// </summary>
    public class GameLoopService : CriticalBackgroundService
    {
        private readonly IGame game;
        private readonly IHostApplicationLifetime lifetime;
        private readonly ILogger<GameLoopService> logger;

        public GameLoopService(IGame game, IHostApplicationLifetime lifetime, ILogger<GameLoopService> logger)
            : base(null)
        {
            this.game = game;
            this.lifetime = lifetime;
            this.logger = logger;
        }

        protected override void OnError(Exception exceptionFromExecuteAsync)
        {
            throw exceptionFromExecuteAsync.LogExceptionMessage(logger);
        }

        protected override async Task ExecuteAsync(CancellationToken token)
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