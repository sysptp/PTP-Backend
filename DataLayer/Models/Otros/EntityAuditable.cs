namespace DataLayer.Models.Otros
{
    public class EntityAuditable
    {
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
