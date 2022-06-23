using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsCrosses.Utils
{
    public static class DIUtils
    {
        public static IServiceCollection AddTypeAndImplementation<TType, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TType
            where TType : class
            =>
            services.AddTransient<TType, TImplementation>().AddTransient<TImplementation>();
    }
}
