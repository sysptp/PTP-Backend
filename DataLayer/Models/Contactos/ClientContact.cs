using DataLayer.Models.Clients;
using DataLayer.Models.Otros;

namespace DataLayer.Models.Contactos
{
    public class ClientContact : EntityAuditable
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int TypeContactId { get; set; }
        public TypeContact? TypeContact { get; set; }
        public string? Value { get; set; }
    }
}
