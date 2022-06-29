using Microsoft.Extensions.DependencyInjection;
using NoughtsCrosses.Classes;
using NoughtsCrosses.Services;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.DI
{
    public static class DIExtensions
    {
        /// <summary>
        /// Adding game classes to specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">Service collection to adding services</param>
        /// <param name="configureBuilder">Optionally builder to configure classes realisations</param>
        /// <param name="addLoopingService">Should <see cref="GameLoopService"/> be added as hosted service. Default true</param>
        /// <returns>ServiceCollection for chaining</returns>
        public static IServiceCollection AddNoughtsCrossesGame(this IServiceCollection services,
            Action<GameBuilder>? configureBuilder = null, bool addLoopingService = true)
        {
            services.AddGameOptions();

            configureBuilder?.Invoke(new GameBuilder(services));

            if (addLoopingService)
                services.AddHostedService<GameLoopService>();

            return services;
        }

        /// <summary>
        /// Adding options classes to ServiceCollection
        /// </summary>
        /// <param name="services">Specified service collection</param>
        /// <returns>ServiceCollection for chaining</returns>
        public static IServiceCollection AddGameOptions(this IServiceCollection services) =>
            services
                .AddOptions<CustomizableField.Configuration>(builder =>
                    builder
                        .BindConfiguration(CustomizableField.Configuration.SectionName)
                        .Validate(CustomizableField.Configuration.Validate))
                .AddOptions<AiPlayer.AiPlayerBehaviour>(builder =>
                    builder
                        .BindConfiguration(AiPlayer.AiPlayerBehaviour.SectionName));
    }
}