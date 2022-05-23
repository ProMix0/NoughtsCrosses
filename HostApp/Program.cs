

using CrossesZeroes.Abstractions;
using CrossesZeroes.Classes;
using CrossesZeroes.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WpfClient;

Console.WriteLine("Building host");

IHost host = Host.CreateDefaultBuilder()
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

        .Configure<AiPlayer.AiPlayerBehaviour>(behaviour => behaviour.wantRepeat = true)

        .AddHostedService<CrossesZeroesLoopService>())

    .ConfigureLogging(logging =>
        logging.ClearProviders())

    .UseConsoleLifetime()

    .Build();

Console.WriteLine("Running host");

await host.RunAsync();

Console.WriteLine("Host shut down");