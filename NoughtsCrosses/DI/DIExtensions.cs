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
        public static IServiceCollection AddNoughtsCrossesGame(this IServiceCollection services, Action<GameBuilder> configureBuilder, bool addLoopingService = true)
        {
            services.AddGameOptions();

            GameBuilder gameBuilder = new();
            configureBuilder(gameBuilder);

            if (gameBuilder.IsValid(out Exception? ex))
            {
                services
                .AddTransient(gameBuilder.player1!)
                .AddTransient(gameBuilder.player2!)
                .AddTransient(gameBuilder.field!)
                .AddTransient(gameBuilder.game!);
            }
            else
                throw ex!;

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
