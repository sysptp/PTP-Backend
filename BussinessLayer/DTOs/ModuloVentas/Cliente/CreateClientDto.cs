namespace BussinessLayer.DTOs.ModuloVentas.Cliente
{
    public class CreateClientDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Identification { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? WebSite { get; set; }
        public string? Description { get; set; }
        public int CodeTypeIdentification { get; set; }
        public long CodeBussines { get; set; }
        public List<ClientContact>? Contacts { get; set; }
        public int AddedBy { get; set; }
    }
}
