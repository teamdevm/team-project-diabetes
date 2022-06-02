using Diabetes.Application.Interfaces;
using Diabetes.Persistence.Contexts;
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
            services.AddDbContext<DataDbContext>(opt=>opt.UseSqlite(configuration.GetConnectionString("Default")));
            services.AddDbContext<AccountContext>(opt=>opt.UseSqlite(configuration.GetConnectionString("Users")));

            services.AddTransient<IGlucoseLevelDbContext, DataDbContext>();
            services.AddTransient<INoteInsulinDbContext, DataDbContext>();
            return services;
        }
    }
}