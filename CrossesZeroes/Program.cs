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

        static CrossesZeroesAbstract ManualCreating()
        {
            return new CrossesZeroesGame(new ConsolePlayer(), new AiPlayer(), new CrossesZeroesField());
        }

        static CrossesZeroesAbstract DICreating()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                    services.AddTransient<CrossesZeroesAbstract, CrossesZeroesWithAi>()
                    .AddTransient<IRealPlayer, ConsolePlayer>()
                    .AddTransient<IAiPlayer, AiPlayer>()
                    .AddTransient<CrossesZeroesField>())
                .Build();

            return host.Services.GetRequiredService<CrossesZeroesAbstract>();
        }
    }
}
