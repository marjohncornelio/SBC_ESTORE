using SBC_ESTORE.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace SBC_ESTORE.Shared.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email Address is a required field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is a required field")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]+$",
                    ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit")]
        public string Password { get; set; } = string.Empty;
        public AccountRoles Role { get; set; }
        public string AvatarURL { get; set; } = string.Empty;
        [Required(ErrorMessage = "Username is a required field")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "PhoneNumber is a required field")]
        [RegularExpression(@"^(09|\+639)\d{9}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNum { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is a required field")]
        public string Address { get; set; } = string.Empty;
    }
}
