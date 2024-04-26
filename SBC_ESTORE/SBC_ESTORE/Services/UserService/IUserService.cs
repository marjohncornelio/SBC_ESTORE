using SBC_ESTORE.Shared.DTO.User;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.UserService
{
    public interface IUserService
    {
        Task<DataResponse<UserSideBarDTO>> GetSideBarUserInfo(int Id);
        Task<GeneralResponse> UploadAvatar(int Id, string AvatarUrl);
        Task<DataResponse<UserDetailsDTO>> GetUserDetails(int Id);
        Task<GeneralResponse> UpdateUserDetails(UserDetailsDTO user, int Id);
        Task<GeneralResponse> UpdatePassword(ChangePasswordDTO password, int Id);

        //Admin
        Task<DataResponse<List<UserDetailsDTO>>> GetAllUsers();

    }
}
