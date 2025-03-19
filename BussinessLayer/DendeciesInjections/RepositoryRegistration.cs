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
using BussinessLayer.Services.SCliente;
using BussinessLayer.Repository.RCampaña;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Repository.Almacen;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Repository.ModuloVentas.RClient;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral;
using BussinessLayer.Interface.Repository.ModuloGeneral;
using BussinessLayer.Repository.ModuloGeneral;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP;
using BussinessLayer.Repository.ModuloGeneral.SMTP;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.DendeciesInjections
{
    //Decorator para inyectar los repositorios
    public static class RepositoryRegistration
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {

            services.AddScoped<IWhatsAppConfigurationRepository, WhatsAppConfigurationRepository>();


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

            services.AddTransient<ICmpClienteRepository,CmpClienteRepository>();
            services.AddTransient<ICmpTipoContactoRepository,CmpTipoContactoRepository>();
            services.AddTransient<ICmpServidoresSmtpRepository,CmpServidoresSmtpRepository>();
            services.AddTransient<ICmpConfiguracionesSmtpRepository, CmpConfiguracionesSmtpRepository>();
            services.AddTransient<ICmpPlantillaRepository, CmpPlantillasRepository>();
            services.AddTransient<ICmpTipoPlantillaRepository, CmpTipoPlantillaRepository>();
            services.AddTransient<ICmpLogsEnvioRepository, CmpLogsEnvioRepository>();
            services.AddTransient<ICmpEstadoRepository, CmpEstadoRepository>();
            services.AddTransient<ICmpCampanaRepository, CmpCampanaRepository>();
            services.AddTransient<ICmpFrecuenciaRepository, CmpFrecuenciaRepository>();
            services.AddTransient<ICmpCampanaDetalleRepository, CmpCampanaDetalleRepository>();
            services.AddTransient<IGnRepeatUnitRepository, GnRepeatUnitRepository>();
            services.AddTransient<IGnSmtpConfiguracionRepository, GnSmtpConfiguracionRepository>();
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

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
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
            services.AddTransient<IAleBitacoraRepository, AleBitacoraRepository>();
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

            services.AddTransient<ICtaAppointmentManagementRepository, CtaAppointmentManagementRepository>();
            services.AddTransient<ICtaAppointmentMovementsRepository, CtaAppointmentMovementsRepository>();
            services.AddTransient<ICtaAppointmentReasonRepository, CtaAppointmentReasonRepository>();
            services.AddTransient<ICtaAppointmentsRepository,CtaAppointmentsRepository>();
            services.AddTransient<ICtaConfiguracionRepository, CtaConfiguracionRepository>();
            services.AddTransient<ICtaEmailConfiguracionRepository, CtaEmailConfiguracionRepository>();
            services.AddTransient<ICtaMeetingPlaceRepository, CtaMeetingPlaceRepository>();
            services.AddTransient<ICtaSessionDetailsRepository, CtaSessionDetailsRepository>();
            services.AddTransient<ICtaSessionsRepository, CtaSessionsRepository>();
            services.AddTransient<ICtaStateRepository, CtaStateRepository>();
            services.AddTransient<ICtaUnwantedRepository,CtaUnwantedRepository>();
            services.AddTransient<ICtaAppointmentSequenceRepository, CtaAppointmentSequenceRepository>();
            services.AddTransient<ICtaAppointmentAreaRepository, CtaAppointmentAreaRepository>();
            services.AddTransient<ICtaAreaXUserRepository, CtaAreaXUserRepository>();
            services.AddTransient<ICtaContactTypeRepository, CtaContactTypeRepository>();
            services.AddTransient<ICtaAppointmentContactsRepository, CtaAppointmentContactsRepository>();
            services.AddTransient<ICtaAppointmentUsersRepository, CtaAppointmentUsersRepository>();
            services.AddTransient<ICtaGuestRepository, CtaGuestRepository>();
            services.AddTransient<ICtaContactRepository, CtaContactRepository>();
            services.AddTransient<ICtaGuestRepository, CtaGuestRepository>();
            services.AddTransient<ICtaAppointmentGuestRepository, CtaAppointmentGuestRepository>();
            services.AddTransient<ICtaEmailTemplatesRepository, CtaEmailTemplatesRepository>();
            services.AddTransient<ICtaNotificationSettingsRepository, CtaNotificationSettingsRepository>();
            services.AddTransient<ICtaParticipantTypesRepository, CtaParticipantTypesRepository>();
            services.AddTransient<ICtaEmailTemplateVariablesRepository, CtaEmailTemplateVariablesRepository>();


            #endregion
        }
    }
}
