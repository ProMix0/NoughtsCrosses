using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CrossesZeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            CrossesZeroesAbstract game;

            game = ManualCreating();
            //while (true)
            //{
            while (game.Turn()) ;
            Console.ReadLine();
            //    game.Restart();
            //}

            game = DICreating();
            //while (true)
            //{
            while (game.Turn()) ;
            Console.ReadLine();
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
                //Добавление сервисов (классов)
                .ConfigureServices(services =>
                    //AddTransient добавляет в коллекцию сервисов класс
                    //Первое обобщение говорит о запрашиваемом классе, второе - о возвращаемом
                    services.AddTransient<CrossesZeroesAbstract, CrossesZeroesWithAi>()
                    .AddTransient<IRealPlayer, ConsolePlayer>()
                    .AddTransient<IAiPlayer, AiPlayer>()
                    .AddTransient<CrossesZeroesField>())
                .Build();

            //Получение сервиса со всеми внедрёнными через конструктор класса зависимостями
            return host.Services.GetRequiredService<CrossesZeroesAbstract>();
        }
    }
}
