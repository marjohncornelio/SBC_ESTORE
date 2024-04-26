using SBC_ESTORE.Shared.DTO.User;

namespace SBC_ESTORE.Client.Services.UserService
{
    public interface IClientUserService
    {
        Task<UserSideBarDTO?> GetUserInfoForSidebar(int id);
        Task UploadAvatar(string AvatarUrl, int id);
        Task<UserDetailsDTO?> GetUserDetails(int Id);
        Task<string?> UpdateUserDetails(UserDetailsDTO user, int Id);
        Task<string?> ChangeUserPassword(ChangePasswordDTO password, int Id);

        //Admin
        Task<List<UserDetailsDTO>?> GetAllUsers();

    }
}
