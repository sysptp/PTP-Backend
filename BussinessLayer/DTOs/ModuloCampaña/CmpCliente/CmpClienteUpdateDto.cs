namespace BussinessLayer.DTOs.ModuloCampaña.CmpCliente
{
    public class CmpClienteUpdateDto 
    {
        public int ClientId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public long EmpresaId { get; set; }
        public string UsuarioModificacion {  get; set; } = string.Empty;
    }
}
