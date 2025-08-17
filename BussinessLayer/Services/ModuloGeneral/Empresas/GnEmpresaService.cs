using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.Services.IAccount;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloGeneral.Empresa;
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
        private readonly IGenericRepository<GnSucursal> _sucursalRepository;
        private readonly IAccountService _accountService;

        public GnEmpresaservice(
            IGnEmpresaRepository repository,
            IMapper mapper,
            IGenericRepository<GnPerfil> perfilRepository,
            IGenericRepository<GnPermiso> permisoRepository,
            IGenericRepository<GnEmpresaXModulo> empresaXModuloRepository,
            IGenericRepository<CtaConfiguration> ctaConfigurationRepository,
            IGenericRepository<GnMenu> menuRepository,
            IGenericRepository<GnSucursal> sucursalRepository,
            IAccountService accountService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _perfilRepository = perfilRepository;
            _permisoRepository = permisoRepository;
            _empresaXModuloRepository = empresaXModuloRepository;
            _ctaConfigurationRepository = ctaConfigurationRepository;
            _menuRepository = menuRepository;
            _sucursalRepository = sucursalRepository;
            _accountService = accountService;
        }

        public override async Task<GnEmpresaResponse> Add(GnEmpresaRequest request)
        {
            var empresa = await base.Add(request);
            await CreateDefaultDataForCompany(empresa.CompanyId);
            return empresa;
        }

        public async Task<CompanyRegistrationResponse> RegisterCompanyWithAdmin(CompanyRegistrationRequest request)
        {
            var empresa = await base.Add(request.Company);

            var sucursal = await CreateDefaultSucursal(empresa.CompanyId, request.Company.CompanyName);

            var perfil = await CreateDefaultProfileForModules(empresa.CompanyId, request.SelectedModuleIds);

            await CreatePermissionsForSelectedModules(perfil.Id, empresa.CompanyId, request.SelectedModuleIds);

            await CreateModulesForCompany(empresa.CompanyId, request.SelectedModuleIds);

            await CreateDefaultConfigurations(empresa.CompanyId);

            var adminUserRequest = MapToRegisterRequest(request.AdminUser, empresa.CompanyId, sucursal.CodigoSuc, (int)perfil.Id);

            var registerResponse = await _accountService.RegisterUserAsync(adminUserRequest, string.Empty);

            if (registerResponse.HasError)
            {
                throw new Exception($"Error al crear el usuario administrador: {registerResponse.Error}");
            }

            var adminUser = _mapper.Map<UserResponse>(adminUserRequest);
            adminUser.Id = 0;
            adminUser.CompanyName = empresa.CompanyName;
            adminUser.RoleName = perfil.Name;
            adminUser.SucursalName = sucursal.NombreSuc;

            return new CompanyRegistrationResponse
            {
                Company = empresa,
                AdminUser = adminUser,
                SucursalName = sucursal.NombreSuc,
                SucursalId = sucursal.CodigoSuc,
                ProfileName = perfil.Name,
                ProfileId = (int)perfil.Id,
                AssignedModuleIds = request.SelectedModuleIds,
                Message = "Empresa, sucursal, perfil de administrador y usuario administrador creados exitosamente"
            };
        }

        private async Task CreateDefaultDataForCompany(long empresaId)
        {
            var perfil = await CreateDefaultProfile(empresaId);
            await CreateDefaultPermissions(perfil.Id, empresaId);
            await CreateDefaultModules(empresaId);
            await CreateDefaultConfigurations(empresaId);
        }

        private async Task<GnSucursal> CreateDefaultSucursal(long empresaId, string companyName)
        {
            var sucursal = new GnSucursal
            {
                CodigoEmp = empresaId,
                NombreSuc = $"{companyName} - Principal",
                Direccion = "Dirección principal",
                Telefono1 = "000-000-0000",
                FechaAdicion = DateTime.Now,
                UsuarioAdicion = "System",
                Borrado = false
            };

            return await _sucursalRepository.Add(sucursal);
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

        private async Task<GnPerfil> CreateDefaultProfileForModules(long empresaId, List<int> moduleIds)
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

        private async Task CreatePermissionsForSelectedModules(long perfilId, long empresaId, List<int> moduleIds)
        {
            var menus = await _menuRepository.GetAll();
            var filteredMenus = menus.Where(m => moduleIds.Contains(m.IdModulo));

            var permisos = filteredMenus.Select(menu => new GnPermiso
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

        private async Task CreateModulesForCompany(long empresaId, List<int> moduleIds)
        {
            var menus = await _menuRepository.GetAll();
            var filteredMenus = menus.Where(m => moduleIds.Contains(m.IdModulo));

            var empresaXModulo = filteredMenus.Select(menu => new GnEmpresaXModulo
            {
                Codigo_EMP = empresaId,
                IDModulo = menu.IdModulo,
                IDMenu = menu.IDMenu,
                Borrado = false,
                FechaAdicion = DateTime.Now,
                UsuarioAdicion = "sistema"
            });

            await _empresaXModuloRepository.AddRangeCompositeKeyAsync(empresaXModulo);
        }

        private async Task CreateDefaultConfigurations(long empresaId)
        {
            var configuration = new CtaConfiguration
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
            };

            await _ctaConfigurationRepository.Add(configuration);
        }

        private RegisterRequest MapToRegisterRequest(AdminUserRegistrationRequest adminUser, long companyId, long sucursalId, int roleId)
        {
            return new RegisterRequest
            {
                FirstName = adminUser.FirstName,
                LastName = adminUser.LastName,
                Email = adminUser.Email,
                UserName = adminUser.UserName,
                Password = adminUser.Password,
                ConfirmPassword = adminUser.ConfirmPassword,
                Phone = adminUser.Phone,
                RoleId = roleId,
                CompanyId = companyId,
                SucursalId = sucursalId,
                UserIP = adminUser.UserIP,
                IsActive = adminUser.IsActive,
                UserImage = adminUser.UserImage,
                LanguageCode = adminUser.LanguageCode ?? "es",
                DefaultUrl = adminUser.DefaultUrl,
                TwoFactorEnabled = adminUser.TwoFactorEnabled
            };
        }
    }
}