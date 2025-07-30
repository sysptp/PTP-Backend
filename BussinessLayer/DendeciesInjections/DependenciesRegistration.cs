using BussinessLayer.Settings;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BussinessLayer.DendeciesInjections
{
    public static class DependenciesRegistration
    {
        public static void AddDependenciesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContex
            services.AddDbContext<PDbContext>(options =>
     options.UseSqlServer(configuration.GetConnectionString("POS_CONN")),
     ServiceLifetime.Scoped);

            services.AddDbContextFactory<PDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("POS_CONN")),
     ServiceLifetime.Scoped);

            #endregion

            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region HttpClient 
            services.AddHttpClient();
            #endregion

        }
    }
}
