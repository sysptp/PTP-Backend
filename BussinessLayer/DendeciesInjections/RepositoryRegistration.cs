using BussinessLayer.Repository.RNcf;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.Repository.ModuloHelpDesk;
using BussinessLayer.Repository.ModuloAuditoria;
using BussinessLayer.Repository.ModuloGeneral.Menu;
using BussinessLayer.Repository.ModuloGeneral.Modulo;
using BussinessLayer.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Menu;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Interfaces.Repository.Configuracion.ParametrosGenerales;
using BussinessLayer.Repository.RConfiguracion.ParametrosGenerales;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Repository.Almacen;

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
            services.AddTransient<IGnScheduleRepository, GnScheduleRepository>();
            services.AddTransient<IGnScheduleUserRepository, GnScheduleUserRepository>();

            #region Geografia

            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IMunicipioRepository, MunicipioRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IProvinciaRepository, ProvinciaRepository>();

            #endregion

            #region Configuracion
            services.AddTransient<IGnMenuRepository, GnMenuRepository>();
            services.AddTransient<IGnModuloRepository, GnModuloRepository>();
            services.AddTransient<IGnPermisoRepository, GnPermisoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IGnParametrosGeneralesRepository, GnParametrosGeneralesRepository>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion

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
            services.AddScoped<IAleBitacoraRepository, AleBitacoraRepository>();
            services.AddTransient<IAleLoginRepository, AleLoginRepository>();
            services.AddTransient<IAleLogsRepository, AleLogsRepository>();
            services.AddTransient<IAlePrintRepository, AlePrintRepository>();
            services.AddTransient<IAleAuditLogRepository, AleAuditLogRepository>();
            services.AddTransient<IAleAuditTableControlRepository, AleAuditTableControlRepository>();
            #endregion

            #region Almacen
            services.AddTransient<IInvAlmacenesRepository, InvAlmacenesRepository>();
            services.AddTransient<IInvAlmacenInventarioRepository, InvAlmacenInventarioRepository>();
            services.AddTransient<IInvInventarioSucursalRepository, InvInventarioSucursalRepository>();
            services.AddTransient<IInvMovAlmacenSucursalRepository, InvMovAlmacenSucursalRepository>();
            services.AddTransient<IInvMovAlmacenSucursalDetalleRepository, InvMovAlmacenSucursalDetalleRepository>();
            services.AddTransient<IInvMovimientoAlmacenRepository, InvMovimientoAlmacenRepository>();
            services.AddTransient<IInvMovimientoAlmacenDetalleRepository, InvMovimientoAlmacenDetalleRepository>();
            services.AddTransient<IInvMovimientoSucursalDetalleRepository, InvMovimientoSucursalDetalleRepository>();
            services.AddTransient<IInvMovInventarioSucursalRepository, InvMovInventarioSucursalRepository>();
            #endregion 

            #region Modulo de Citas

            services.AddScoped<ICtaAppointmentManagementRepository, CtaAppointmentManagementRepository>();
            services.AddScoped<ICtaAppointmentMovementsRepository, CtaAppointmentMovementsRepository>();
            services.AddScoped<ICtaAppointmentReasonRepository, CtaAppointmentReasonRepository>();
            services.AddScoped<ICtaAppointmentsRepository,CtaAppointmentsRepository>();
            services.AddScoped<ICtaConfiguracionRepository, CtaConfiguracionRepository>();
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
