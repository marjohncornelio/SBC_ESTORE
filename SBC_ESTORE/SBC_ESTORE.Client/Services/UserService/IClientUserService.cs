using SBC_ESTORE.Shared.DTO.User;

namespace SBC_ESTORE.Client.Services.UserService
{
    public interface IClientUserService
    {
        Task<UserSideBarDTO?> GetUserInfoForSidebar(int id);
        Task UploadAvatar(string AvatarUrl, int id);
    }
}
