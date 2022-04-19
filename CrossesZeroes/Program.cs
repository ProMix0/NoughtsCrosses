using CrossesZeroes.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace CrossesZeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            CrossesZeroesAbstract game;

            /*game = ManualCreating();
            //while (true)
            //{
            while (game.Turn()) ;
            Console.ReadKey();
            //    game.Restart();
            //}*/

            game = DICreating();
            //while (true)
            //{
            while (game.Turn()) ;
            Console.ReadKey();
            //    game.Restart();
            //}
        }

        /// <summary>
        /// Создание иерархии объектов вручную
        /// </summary>
        /// <returns></returns>
        static CrossesZeroesAbstract ManualCreating()
        {
            return new CrossesZeroesGame(new ConsolePlayer(), new AiPlayer(), new CrossesZeroesField());
        }

        /// <summary>
        /// Создание иерархии с помощью инъекции зависимостей
        /// </summary>
        /// <returns></returns>
        static CrossesZeroesAbstract DICreating()
        {
            //Создание хоста
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder =>
                    builder.AddJsonFile("Settings.json"))
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
                    .AddTransient<IRealPlayer, ConsolePlayer>()
                    .AddTransient<IAiPlayer, AiPlayer>()
                    .AddTransient<ICrossesZeroesField, CustomizableField>())
                .Build();

            //Получение сервиса со всеми внедрёнными через конструктор класса зависимостями
            return host.Services.GetRequiredService<CrossesZeroesAbstract>();
        }
    }
}
