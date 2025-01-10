namespace BussinessLayer.DTOs.ModuloCampaña.CmpCliente
{
    public class CmpClientCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public long EmpresaId { get; set; }
        public string? UsuarioCreacion {  get; set; }

    }
}
