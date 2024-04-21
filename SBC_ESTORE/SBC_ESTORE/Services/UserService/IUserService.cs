using SBC_ESTORE.Shared.DTO.User;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.UserService
{
    public interface IUserService
    {
        Task<DataResponse<UserSideBarDTO>> GetSideBarUserInfo(int Id);
        Task<GeneralResponse> UploadAvatar(int Id, string AvatarUrl);
    }
}
