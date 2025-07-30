using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Core.Services;
using disasterApi.Infra.Database;
using disasterApi.Infra.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace disasterApi.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString));
        }
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureRedisCache(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = config.GetConnectionString("Redis");
            });
        }
    }
}
