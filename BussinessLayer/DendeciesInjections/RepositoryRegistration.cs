using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Interfaces.Repository.Geografia;
using BussinessLayer.Repository.REmpresa;
using BussinessLayer.Repository.RGeografia;
using BussinessLayer.Repository.RNcf;
using BussinessLayer.Repository.RSeguridad;
using BussinessLayer.Services.SGeografia;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.DendeciesInjections
{
    //Decorator para inyectar los repositorios
    public static class RepositoryRegistration
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddTransient<IGnPerfilRepository, GnPerfilRepository>();
            services.AddTransient<IGnEmpresaRepository, GnEmpresaRepository>();
            services.AddTransient<IGnSucursalRepository, GnSucursalRepository>();
            services.AddTransient<INcfRepository, NcfRepository>();
            #region Geografia

            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IMunicipioRepository, MunicipioRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IProvinciaRepository, ProvinciaRepository>();

            #endregion
        }
    }
}
