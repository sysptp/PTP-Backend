using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.ModuloGeneral.Empresas
{
    public class AdminUserRegistrationRequest
    {
        [SwaggerSchema("The first name of the user.")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = null!;

        [SwaggerSchema("The last name of the user.")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = null!;

        [SwaggerSchema("The email address of the user.")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [SwaggerSchema("The username of the user.")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = null!;

        [SwaggerSchema("The password for the account.")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = null!;

        [SwaggerSchema("The confirmation of the password.")]
        [Required(ErrorMessage = "Password confirmation is required")]
        public string ConfirmPassword { get; set; } = null!;

        [SwaggerSchema("The phone number of the user.")]
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = null!;

        [SwaggerSchema("The IP address of the user.")]
        public string? UserIP { get; set; }

        [SwaggerSchema("Indicates if the user is active.")]
        public bool IsActive { get; set; } = true;

        [SwaggerSchema("The user profile image.")]
        public string? UserImage { get; set; }

        [SwaggerSchema("The language code for the user.")]
        public string? LanguageCode { get; set; } = "es";

        [SwaggerSchema("The default URL for the user.")]
        public string? DefaultUrl { get; set; }

        [SwaggerSchema("Indicates if two-factor authentication is enabled.")]
        public bool TwoFactorEnabled { get; set; } = false;
    }
}