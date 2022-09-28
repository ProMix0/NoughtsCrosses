using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Utils
{
    public static class OptionsUtils
    {
        /// <summary>
        /// Overload to more fluent register options
        /// </summary>
        /// <param name="services">ServiceCollection to register</param>
        /// <param name="action">Configuration of <see cref="OptionsBuilder{TOptions}"/></param>
        /// <typeparam name="TOptions">Options type to add</typeparam>
        /// <returns>ServiceCollection to chaining</returns>
        public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services,
            Action<OptionsBuilder<TOptions>> action)
            where TOptions : class
        {
            action(services.AddOptions<TOptions>());
            return services;
        }
    }
}