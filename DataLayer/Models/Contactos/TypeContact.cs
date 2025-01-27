using DataLayer.Models.Otros;

namespace DataLayer.Models.Contactos
{
    public class TypeContact : EntityAuditable
    {
        public int Id { get; set; }
        public int BussinesId { get; set; }
        public string? Format {  get; set; }
        public string? Description {  get; set; }
        public ICollection<ClientContact>? ClientContacts { get; set; }
    }
}
