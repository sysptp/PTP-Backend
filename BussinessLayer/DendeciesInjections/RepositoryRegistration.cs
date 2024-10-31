using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Repository.REmpresa;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.DendeciesInjections
{
    public static class RepositoryRegistration
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            //services.AddTransient<IGnPerfilRepository, IGnPerfilRepository>();
            services.AddTransient<ISC_EMP001Repository,SC_EMP001Repository>();
        }
    }
}
