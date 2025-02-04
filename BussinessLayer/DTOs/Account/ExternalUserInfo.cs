
namespace BussinessLayer.DTOs.Account
{
    public class ExternalUserInfo
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ProviderUserId { get; set; } = null!;
    };

}
