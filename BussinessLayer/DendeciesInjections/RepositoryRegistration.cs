using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using BussinessLayer.Interfaces.Repository.Configuracion.Modulo;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Interfaces.Repository.Geografia;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.Repository.HelpDesk;
using BussinessLayer.Repository.RConfiguracion.Menu;
using BussinessLayer.Repository.RConfiguracion.Modulo;
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

            #region Configuracion
            services.AddTransient<IGnMenuRepository,GnMenuRepository>();
            services.AddTransient<IGnModuloRepository,GnModuloRepository>();
            #endregion

            #region HelpDesk
            services.AddTransient<IHdkCategoryTicketRepository, HdkCategoryTicketRepository>();
            services.AddTransient<IHdkDepartamentsRepository, HdkDepartamentsRepository>();
            services.AddTransient<IHdkDepartXUsuarioRepository, HdkDepartXUsuarioRepository>();
            services.AddTransient<IHdkErrorSubCategoryRepository, HdkErrorSubCategoryRepository>();
            services.AddTransient<IHdkFileEvidenceTicketRepository, HdkFileEvidenceTicketRepository>();
            services.AddTransient<IHdkNoteTicketRepository, HdkNoteTicketRepository>();
            services.AddTransient<IHdkPrioridadTicketRepository, HdkPrioridadTicketRepository>();
            services.AddTransient<IHdkSolutionTicketRepository, HdkSolutionTicketRepository>();
            services.AddTransient<IHdkStatusTicketRepository, HdkStatusTicketRepository>();
            services.AddTransient<IHdkSubCategoryRepository, HdkSubCategoryRepository>();
            services.AddTransient<IHdkTicketsRepository, HdkTicketsRepository>();
            services.AddTransient<IHdkTypeTicketRepository, HdkTypeTicketRepository>();
            #endregion
        }
    }
}
