using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataLayer.Models.Clients
{
    public class Client
    {
        public int Id { get; set; }
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
        public bool IsDeleted { get; set; }
        public string? AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
    }
}
