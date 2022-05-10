using CrossesZeroes.Classes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossesZeroes.Services;
using CrossesZeroes.Abstractions;

namespace CrossesZeroes.DI
{
    public static class DIExtensions
    {
        public static IHostBuilder AddCrossesZeroesGame(this IHostBuilder builder) =>
            builder
                .ConfigureAppConfiguration(config =>
                    config.AddJsonFile("Settings.json"))

                //Добавление сервисов (классов)
                .ConfigureServices((context, services) =>

                    //Позволят получить объект с настройками через DI
                    services
                    .AddOptions<CustomizableField.Configuration>()
                        .BindConfiguration(CustomizableField.Configuration.SectionName)
                        .Validate(CustomizableField.Configuration.Validate)
                        .Services

                    //AddTransient добавляет в коллекцию сервисов класс
                    //Первое обобщение говорит о запрашиваемом классе, второе - о возвращаемом
                    .AddTransient<CrossesZeroesAbstract, CrossesZeroesWithAi>()
                    //.AddTransient<IRealPlayer, WpfPlayer>()
                    .AddTransient<IAiPlayer, AiPlayer>()
                    .AddTransient<ICrossesZeroesField, ExtraCustomizableField>()

                    //TODO inject by single metod
                    //.AddTransient<WpfClient.WpfClient>()

                    .AddHostedService<CrossesZeroesLoopService>())

                .ConfigureLogging(logging =>
                    logging.ClearProviders());
    }
}
