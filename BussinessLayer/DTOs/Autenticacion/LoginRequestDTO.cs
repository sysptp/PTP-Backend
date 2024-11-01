namespace BussinessLayer.DTOs.Autenticacion
{
    public class LoginRequestDTO
    {
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
