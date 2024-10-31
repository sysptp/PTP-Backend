using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BussinessLayer.DendeciesInjections
{
    public static class DependenciesRegistration
    {
        public static void AddDependenciesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContex
            services.AddDbContext<PDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("POS_CONN")));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

        }
    }
}
