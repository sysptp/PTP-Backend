using AutoMapper;
using DataLayer.Models.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad.IpWhois;

namespace BussinessLayer.Services.ModuloAuditoria
{
    public class AleBitacoraService : GenericService<AleBitacoraRequest, AleBitacoraReponse, AleAuditoria>, IAleBitacoraService
    {
        private readonly IAleBitacoraRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;
        private readonly IIpGeolocalitationService _ipWhoisService;

        public AleBitacoraService(
            IAleBitacoraRepository repository,
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
        /// Objeto <see cref="AleBitacoraRequest"/> que contiene los datos de auditoría, como el módulo,
        /// acción, IP, y otros detalles.
        /// </param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica, con un objeto <see cref="AleBitacoraReponse"/> 
        /// que indica el resultado del registro de auditoría.
        /// </returns>
        public async Task AddAuditoria(AleBitacoraRequest vm)
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

            await _repository.AddAuditoria(model);
        }
        /// <summary>
        /// Método para obtener una lista de auditorías filtrada por diversos criterios.
        /// Este método permite aplicar múltiples filtros opcionales, como módulo, acción,
        /// fecha y hora, cadenas de búsqueda en los requests y responses, rol de usuario, 
        /// empresa, y sucursal, para obtener registros específicos según las necesidades del sistema.
        /// </summary>
        /// <param name="requestLike">
        /// Cadena para buscar coincidencias parciales en el contenido del request. Este filtro es opcional.
        /// </param>
        /// <param name="responseLike">
        /// Cadena para buscar coincidencias parciales en el contenido del response. Este filtro es opcional.
        /// </param>
        /// <param name="rolUsuario">
        /// Rol del usuario que realizó la acción. Este filtro es opcional.
        /// <returns>
        /// Una tarea que representa la operación asincrónica y devuelve una lista de objetos 
        /// <see cref="AleBitacoraReponse"/> que cumplen con los criterios de filtrado.
        /// </returns>

        public async Task<List<AleBitacoraReponse>> GetAllByFilters(
            string modulo,
            string accion,
            int ano,
            int mes,
            int dia,
            int hora,
            string requestLike,
            string responseLike,
            string rolUsuario,
            long idEmpresa,
            long idSucursal)
        {
            var auditoriaList = await GetAllDto();
            var query = auditoriaList.AsQueryable();

            if (!string.IsNullOrEmpty(modulo))
                query = query.Where(x => x.Modulo.Contains(modulo));

            if (!string.IsNullOrEmpty(accion))
                query = query.Where(x => x.Acccion.Contains(accion));

            if (ano > 0)
                query = query.Where(x => x.Ano == ano);

            if (mes > 0)
                query = query.Where(x => x.Mes == mes);

            if (dia > 0)
                query = query.Where(x => x.Dia == dia);

            if (hora > 0)
                query = query.Where(x => x.Hora == hora);

            if (!string.IsNullOrEmpty(requestLike))
                query = query.Where(x => x.Request.Contains(requestLike));

            if (!string.IsNullOrEmpty(responseLike))
                query = query.Where(x => x.Response.Contains(responseLike));

            if (!string.IsNullOrEmpty(rolUsuario))
                query = query.Where(x => x.RolUsuario == rolUsuario);

            if (idEmpresa > 0)
                query = query.Where(x => x.IdEmpresa == idEmpresa);

            if (idSucursal > 0)
                query = query.Where(x => x.IdSucursal == idSucursal);

            return query.ToList();
        }


    }
}
