using Diabetes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Diabetes.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DefaultConnection"];
            services.AddDbContext<DataDbContext>(opt=>opt.UseSqlite(connectionString));
            services.AddTransient<IGlucoseLevelDbContext, DataDbContext>();
            services.AddTransient<INoteInsulinDbContext, DataDbContext>();
            return services;
        }
    }
}