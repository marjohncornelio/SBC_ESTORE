using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.Enum;

namespace SBC_ESTORE.Shared.DTO.User
{
    public class UserSideBarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public AccountRoles Role { get; set; }
        public string AvatarURL { get; set; } = string.Empty;
        public CartDTO? Cart { get; set; }
    }
}
