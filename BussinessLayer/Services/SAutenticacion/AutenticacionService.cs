using BussinessLayer.DTOs.Autenticacion;
using BussinessLayer.Helpers.UtilsHelpers;
using BussinessLayer.Interfaces.IAutenticacion;
using DataLayer.PDbContex;
using System.Linq;
using System.Net.NetworkInformation;

namespace BussinessLayer.Services.SAutenticacion
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly PDbContext _context;

        public AutenticacionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public UserSectionDto IniciarSesion(string usuario, string password)
        {
            var userdata = _context?.SC_USUAR001
                .FirstOrDefault(x => x.USUARIO == usuario && x.PASSWOR == password);

            var sucursalUsuario = _context.SC_SUC001
                .FirstOrDefault(x => x.ESTADO_SUC == true && x.CODIGO_SUC == userdata.CODIGO_SUC);

            var usuarioEmpresa = _context.SC_EMP001
                .FirstOrDefault(x => x.CODIGO_EMP == userdata.CODIGO_EMP);

            var ip = Utilidades.GetIP(NetworkInterfaceType.Wireless80211, "Fisica").ToString();


            return new UserSectionDto { 
                DatosEmpresa = usuarioEmpresa, 
                Ip = ip, 
                DatosUsuario = userdata, 
                DatosSucursal = sucursalUsuario 
            };

        }
    }
}
