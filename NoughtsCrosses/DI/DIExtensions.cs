using NoughtsCrosses.Classes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Services;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.DI
{
    public static class DIExtensions
    {
        public static IServiceCollection AddNoughtsCrossesGame(this IServiceCollection services, Action<GameBuilder>? configureBuilder = null, bool addLoopingService = true)
        {
            services.AddGameOptions();

            configureBuilder?.Invoke(new GameBuilder(services));

            if (addLoopingService)
                services.AddHostedService<GameLoopService>();

            return services;
        }



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
