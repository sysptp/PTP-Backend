
namespace BussinessLayer.DTOs.Account
{
    public class ExternalRegisterRequest
    {
        /// <summary>
        /// Proveedor de autenticación externa (Google, Microsoft, Facebook).
        /// </summary>
        public int Provider { get; set; } 

        /// <summary>
        /// Token de autenticación proporcionado por el proveedor externo.
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// ID de la empresa asociada al usuario.
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// ID de la sucursal asociada al usuario.
        /// </summary>
        public long SucursalId { get; set; }

        /// <summary>
        /// ID del rol asociado al usuario.
        /// </summary>
        public int RoleId { get; set; }
    }

}
