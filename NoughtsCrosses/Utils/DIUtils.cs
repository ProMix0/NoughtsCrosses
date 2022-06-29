using BetterHostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace NoughtsCrosses.Utils
{
    public static class DIUtils
    {
        /// <summary>
        /// Register service both as <see cref="TType"/> and <see cref="TImplementation"/>
        /// </summary>
        /// <param name="services">Specified ServiceCollection to add</param>
        /// <typeparam name="TType">Interface/abstract class type</typeparam>
        /// <typeparam name="TImplementation">Implementation type</typeparam>
        /// <returns>ServiceCollection to chaining</returns>
        public static IServiceCollection AddTypeAndImplementation<TType, TImplementation>(
            this IServiceCollection services)
            where TImplementation : class, TType
            where TType : class
            =>
                services.AddTransient<TType, TImplementation>().AddTransient<TImplementation>();
    }

    /// <summary>
    /// CriticalBackgroundService without app ending behavior
    /// </summary>
    public abstract class NotEndingBackgroundService : CriticalBackgroundService
    {
        private static readonly FalseApplicationEnder ender = new();

        public NotEndingBackgroundService() : base(ender)
        {
        }

        protected abstract override Task ExecuteAsync(CancellationToken stoppingToken);

        private class FalseApplicationEnder : IApplicationEnder
        {
            public void ShutDownApplication()
            {
            }
        }
    }
}