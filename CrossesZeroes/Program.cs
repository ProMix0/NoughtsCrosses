using CrossesZeroes.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using CrossesZeroes.Services;
using System.Threading.Tasks;
using CrossesZeroes.Abstractions;
using WpfClient;

namespace CrossesZeroes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Создание хоста
            await Host.CreateDefaultBuilder()

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
                    .AddTransient<IRealPlayer, WpfPlayer>()
                    .AddTransient<IAiPlayer, AiPlayer>()
                    .AddTransient<ICrossesZeroesField, ExtraCustomizableField>()

                    //TODO inject by single metod
                    .AddTransient<WpfClient.WpfClient>()

                    .AddHostedService<CrossesZeroesLoopService>())

                .ConfigureLogging(logging =>
                    logging.ClearProviders())

                .RunConsoleAsync();
            //TODO shutdown after all workers done
        }
    }
}
