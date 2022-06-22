using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoughtsCrosses.Utils
{
    public static class OptionsUtils
    {
        public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, Action<OptionsBuilder<TOptions>> action)
            where TOptions : class
        {
            action(services.AddOptions<TOptions>());
            return services;
        }
    }
}
