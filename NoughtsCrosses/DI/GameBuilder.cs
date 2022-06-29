using Microsoft.Extensions.DependencyInjection;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.DI
{
    /// <summary>
    /// Class for configure game services
    /// </summary>
    public class GameBuilder
    {
        private readonly IServiceCollection services;

        internal GameBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        /// <summary>
        /// Add specified player to container
        /// </summary>
        /// <typeparam name="TPlayer">Player type</typeparam>
        /// <returns>GameBuilder to chaining</returns>
        public GameBuilder AddPlayer<TPlayer>()
            where TPlayer : class, IPlayer
        {
            services.AddTypeAndImplementation<IPlayer, TPlayer>();

            return this;
        }

        /// <summary>
        /// Add specified field to container
        /// </summary>
        /// <typeparam name="TField">Field type</typeparam>
        /// <returns>GameBuilder to chaining</returns>
        public GameBuilder AddField<TField>()
            where TField : class, IGameField
        {
            services.AddTypeAndImplementation<IGameField, TField>();

            return this;
        }

        /// <summary>
        /// Add specified game to container
        /// </summary>
        /// <typeparam name="TGame">Game type</typeparam>
        /// <returns>GameBuilder to chaining</returns>
        public GameBuilder AddGame<TGame>()
            where TGame : class, IGame
        {
            services.AddTypeAndImplementation<IGame, TGame>();

            return this;
        }
    }
}