using AutoMapper;
using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Interfaces.Repository.Auditoria;
using BussinessLayer.Interfaces.Helpers;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Interfaces.ISeguridad;

namespace BussinessLayer.Services.SAuditoria
{
    public class AleAuditoriaService : GenericService<AleAuditoriaRequest, AleAuditoriaReponse, AleAuditoria>, IAleAuditoriaService
    {
        private readonly IAleAuditoriaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;
        private readonly IIpGeolocalitationService _ipWhoisService;

        public AleAuditoriaService(
            IAleAuditoriaRepository repository,
            IMapper mapper,
            ITokenService tokenService,
            IUsuarioService usuarioService,
            IIpGeolocalitationService ipWhoisService)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _usuarioService = usuarioService;
            _ipWhoisService = ipWhoisService;
        }

        /// <summary>
        /// Método de auditoría para registrar las acciones realizadas por los usuarios en el sistema PTP.
        /// Este método se utiliza para empresas que integran tanto el API como el sistema PTP, registrando
        /// información relevante como el usuario, empresa, sucursal, ubicación geográfica, y más.
        /// </summary>
        /// <param name="vm">
        /// Objeto <see cref="AleAuditoriaRequest"/> que contiene los datos de auditoría, como el módulo,
        /// acción, IP, y otros detalles.
        /// </param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica, con un objeto <see cref="AleAuditoriaReponse"/> 
        /// que indica el resultado del registro de auditoría.
        /// </returns>
        public async Task AddAuditoria(AleAuditoriaRequest vm)
        {
            var usuario = await _usuarioService.GetByUserNameResponse(vm.UserName);

            vm.RolUsuario = usuario?.RoleName ?? "Unknown";

            vm.IdEmpresa = usuario.CompanyId;
            vm.IdSucursal = usuario.SucursalId;

            if (!string.IsNullOrWhiteSpace(vm.IP) && vm.IP != "Unknown")
            {
                var coordinates = await _ipWhoisService.GetCoordinatesAsync(vm.IP);
                vm.Latitud = coordinates.Latitude;
                vm.Longitud = coordinates.Longitude;
            }

            var model = _mapper.Map<AleAuditoria>(vm);
            model.UsuarioAdicion = vm.UserName;

            await _repository.Add(model);
        }

    }
}
