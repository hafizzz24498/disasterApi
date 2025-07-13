using disasterApi.Core.Interfaces.Services;
using disasterApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace disasterApi.Core.Extensions
{
    public static class ServiceCollectionExtensions

    {
        public static IServiceCollection AddCoreDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IRegionService, RegionService>();

            return services;
        }
    }
}
