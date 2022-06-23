using Microsoft.Extensions.DependencyInjection;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.DI
{
    public class GameBuilder
    {
        private readonly IServiceCollection services;

        internal GameBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public GameBuilder UsePlayer<TPlayer>()
            where TPlayer : class, IPlayer
        {
            services.AddTypeAndImplementation<IPlayer, TPlayer>();

            return this;
        }

        public GameBuilder UseField<TField>()
            where TField : class, IGameField
        {
            services.AddTypeAndImplementation<IGameField, TField>();

            return this;
        }

        public GameBuilder UseGame<TGame>()
            where TGame : class, IGame
        {
            services.AddTypeAndImplementation<IGame, TGame>();

            return this;
        }
    }
}
