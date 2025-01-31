
using DataLayer.Models.ModuloGeneral.Empresa;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.ModuloCitas.CtaGuest
{
    public class CtaGuestResponse
    {
        public int Id { get; set; }
        public string Names { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? NickName { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set;} 
    }
}
