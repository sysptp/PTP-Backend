
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Account
{
    public class TwoFactorVerificationRequest
    {
        public long UserId { get; set; } 
        public string Code { get; set; } = null!;
    }
}
