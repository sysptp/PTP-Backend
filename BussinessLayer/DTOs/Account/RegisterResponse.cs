namespace BussinessLayer.DTOs.Account
{
    public class RegisterResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public int? UserId { get; set; }
    }
}
