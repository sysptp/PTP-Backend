using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.REmpresa;
using BussinessLayer.Repository.RSeguridad;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.DendeciesInjections
{
    public static class RepositoryRegistration
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddTransient<IGnPerfilRepository, GnPerfilRepository>();
            services.AddTransient<IGnEmpresaRepository,GnEmpresaRepository>();
        }
    }
}
