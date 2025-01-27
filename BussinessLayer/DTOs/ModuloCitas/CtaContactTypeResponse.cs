namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaContactTypeResponse
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
