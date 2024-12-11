using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using BussinessLayer.Interfaces.Repository.Configuracion.Modulo;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Interfaces.Repository.Geografia;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.Repository.HelpDesk;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Repository.RConfiguracion.Menu;
using BussinessLayer.Repository.RConfiguracion.Modulo;
using BussinessLayer.Repository.REmpresa;
using BussinessLayer.Repository.RGeografia;
using BussinessLayer.Repository.RNcf;
using BussinessLayer.Repository.RSeguridad;
using BussinessLayer.Services.SGeografia;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.Interfaces.Repository.Auditoria;
using BussinessLayer.Repository.Auditoria;
using DataLayer.Models.ModuloGeneral;
using BussinessLayer.Interfaces.Repository.Configuracion.ParametrosGenerales;
using BussinessLayer.Repository.RConfiguracion.ParametrosGenerales;
using BussinessLayer.Repository.RClient;
using BussinessLayer.Services.SCliente;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.Modulo_Citas;

namespace BussinessLayer.DendeciesInjections
{
    //Decorator para inyectar los repositorios
    public static class RepositoryRegistration
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddTransient<INcfRepository, NcfRepository>();

            #region Modulo General 
            services.AddTransient<IGnPerfilRepository, GnPerfilRepository>();
            services.AddTransient<IGnEmpresaRepository, GnEmpresaRepository>();
            services.AddTransient<IGnSucursalRepository, GnSucursalRepository>();
            services.AddTransient<INcfRepository, NcfRepository>();
            services.AddTransient<IClientRepository,ClientRepository>();
            services.AddTransient<IClientContactRepository,ClientContactRepository>();
            services.AddTransient<IGnScheduleRepository, GnScheduleRepository>();
            services.AddTransient<IGnScheduleUserRepository, GnScheduleUserRepository>();
            #endregion

            #region Geografia

            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IMunicipioRepository, MunicipioRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IProvinciaRepository, ProvinciaRepository>();

            #endregion

            #region Configuracion
            services.AddTransient<IGnMenuRepository,GnMenuRepository>();
            services.AddTransient<IGnModuloRepository,GnModuloRepository>();
            services.AddTransient<IGnPermisoRepository, GnPermisoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IGnParametrosGeneralesRepository, GnParametrosGeneralesRepository>();
            
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
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

            #region Auditoria
            services.AddScoped<IAleAuditoriaRepository, AleAuditoriaRepository>();
            services.AddTransient<IAleLoginRepository, AleLoginRepository>();
            services.AddTransient<IAleLogsRepository, AleLogsRepository>();
            services.AddTransient<IAlePrintRepository, AlePrintRepository>();

            #endregion

            #region Modulo de Citas

            services.AddScoped<ICtaAppointmentManagementRepository, CtaAppointmentManagementRepository>();
            services.AddScoped<ICtaAppointmentMovementsRepository, CtaAppointmentMovementsRepository>();
            services.AddScoped<ICtaAppointmentReasonRepository, CtaAppointmentReasonRepository>();
            services.AddScoped<ICtaAppointmentsRepository,CtaAppointmentsRepository>();
            services.AddScoped<ICtaCitaConfiguracionRepository, CtaCitaConfiguracionRepository>();
            services.AddScoped<ICtaEmailConfiguracionRepository, CtaEmailConfiguracionRepository>();
            services.AddScoped<ICtaMeetingPlaceRepository, CtaMeetingPlaceRepository>();
            services.AddScoped<ICtaSessionDetailsRepository, CtaSessionDetailsRepository>();
            services.AddScoped<ICtaSessionsRepository, CtaSessionsRepository>();
            services.AddScoped<ICtaStateRepository, CtaStateRepository>();
            services.AddScoped<ICtaUnwantedRepository,CtaUnwantedRepository>();

            #endregion
        }
    }
}
