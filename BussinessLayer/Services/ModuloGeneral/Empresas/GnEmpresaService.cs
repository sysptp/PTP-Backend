using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using DataLayer.Models.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloGeneral.Menu;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Services.ModuloGeneral.Empresas
{
    public class GnEmpresaservice : GenericService<GnEmpresaRequest, GnEmpresaResponse, GnEmpresa>, IGnEmpresaservice
    {
        private readonly IGnEmpresaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<GnPerfil> _perfilRepository;
        private readonly IGenericRepository<GnPermiso> _permisoRepository;
        private readonly IGenericRepository<GnEmpresaXModulo> _empresaXModuloRepository;
        private readonly IGenericRepository<CtaConfiguration> _ctaConfigurationRepository;
        private readonly IGenericRepository<GnMenu> _menuRepository;

        public GnEmpresaservice(
            IGnEmpresaRepository repository,
            IMapper mapper,
            IGenericRepository<GnPerfil> perfilRepository,
            IGenericRepository<GnPermiso> permisoRepository,
            IGenericRepository<GnEmpresaXModulo> empresaXModuloRepository,
            IGenericRepository<CtaConfiguration> ctaConfigurationRepository,
            IGenericRepository<GnMenu> menuRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _perfilRepository = perfilRepository;
            _permisoRepository = permisoRepository;
            _empresaXModuloRepository = empresaXModuloRepository;
            _ctaConfigurationRepository = ctaConfigurationRepository;
            _menuRepository = menuRepository;
        }

        public override async Task<GnEmpresaResponse> Add(GnEmpresaRequest request)
        {
            var empresa = await base.Add(request);

            await CreateDefaultDataForCompany(empresa.CompanyId);

            return empresa;
        }

        private async Task CreateDefaultDataForCompany(long empresaId)
        {
            var perfil = await CreateDefaultProfile(empresaId);

            await CreateDefaultPermissions(perfil.Id, empresaId);

            await CreateDefaultModules(empresaId);

            await CreateDefaultConfigurations(empresaId);
        }


        private async Task<GnPerfil> CreateDefaultProfile(long empresaId)
        {
            var existingPerfil = await _perfilRepository.GetAll();
            var adminPerfil = existingPerfil.FirstOrDefault(p =>
                p.Name == "admin" && p.IDEmpresa == empresaId);

            if (adminPerfil != null)
            {
                return adminPerfil;
            }

            var uniqueName = $"admin_{empresaId}";
            var uniqueNormalizedName = uniqueName.ToUpper();

            var existingUnique = existingPerfil.FirstOrDefault(p =>
                p.Name == uniqueName);

            if (existingUnique != null)
            {
                return existingUnique;
            }

            var perfil = new GnPerfil
            {
                Descripcion = "administrador del sistema",
                IDEmpresa = empresaId,
                FechaAdicion = DateTime.Now,
                UsuarioAdicion = "System",
                Borrado = false,
                Name = uniqueName,
                NormalizedName = uniqueNormalizedName
            };

            return await _perfilRepository.Add(perfil);
        }

        private async Task CreateDefaultPermissions(long perfilId, long empresaId)
        {
            var menus = await _menuRepository.GetAll();

            var permisos = menus.Select(menu => new GnPermiso
            {
                IDPerfil = (int)perfilId,
                IDMenu = menu.IDMenu,
                Codigo_EMP = empresaId,
                Crear = true,
                Eliminar = true,
                Editar = true,
                Consultar = true,
                FechaAdicion = DateTime.Now,
                UsuarioAdicion = "sistema",
                Borrado = false
            });

            await _permisoRepository.AddRangeAsync(permisos);
        }

        private async Task CreateDefaultModules(long empresaId)
        {
            var menus = await _menuRepository.GetAll();

            var empresaXModulo = menus.Select(menu => new GnEmpresaXModulo
            {
                
                Codigo_EMP = empresaId,
                IDModulo = 9,
                IDMenu = menu.IDMenu,
                Borrado = false,
                FechaAdicion = DateTime.Now,
                UsuarioAdicion = "sistema"
            });

            await _empresaXModuloRepository.AddRangeCompositeKeyAsync(empresaXModulo);
        }

        private async Task CreateDefaultConfigurations(long empresaId)
        {
            var configuraciones = new List<CtaConfiguration>();

            for (int i = 0; i < 3; i++)
            {
                configuraciones.Add(new CtaConfiguration
                {
                    SendEmail = true,
                    SendSms = false,
                    SendEmailReminder = false,
                    SendSmsReminder = false,
                    SendWhatsapp = false,
                    SendWhatsappReminder = false,
                    Borrado = false,
                    FechaAdicion = DateTime.Now,
                    UsuarioAdicion = "dtest",
                    FechaModificacion = DateTime.Now,
                    UsuarioModificacion = "anthony0010",
                    CompanyId = empresaId,
                    NotifyClosure = true,
                    DaysInAdvance = 1
                });
            }

            await _ctaConfigurationRepository.AddRangeAsync(configuraciones);

        }
    }
}
