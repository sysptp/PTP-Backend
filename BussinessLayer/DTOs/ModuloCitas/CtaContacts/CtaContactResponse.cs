
namespace BussinessLayer.DTOs.ModuloCitas.CtaContacts
{
    public class CtaContactResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public int ContactTypeId { get; set; }
        public string? ContactTypeDescription { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }

    }
}
