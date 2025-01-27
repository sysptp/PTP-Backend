namespace BussinessLayer.DTOs.ModuloCampaña.CmpCliente
{
    public class CmpClienteDto
    {
        public int ClientId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public long EmpresaId { get; set; }
    }
}
