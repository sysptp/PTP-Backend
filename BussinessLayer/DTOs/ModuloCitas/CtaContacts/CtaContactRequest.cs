namespace BussinessLayer.DTOs.ModuloCitas.CtaContacts
{
    public class CtaContactRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public int ContactTypeId { get; set; }
        public long CompanyId { get; set; }
    }
}
