using SBC_ESTORE.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace SBC_ESTORE.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public AccountRoles? Role { get; set; } = AccountRoles.USER;
        public string AvatarURL { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
