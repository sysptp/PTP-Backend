namespace BussinessLayer.DTOs.Account
{
    public class RegisterResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? UserId { get; set; }
    }
}
