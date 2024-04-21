using SBC_ESTORE.Shared.DTO.User;

namespace SBC_ESTORE.Client.Services.AuthServices
{
    public interface IClientAuthService
    {
        Task<string?> LoginAccount(LoginDTO user);
        Task<string?> RegisterAccount(UserDTO user);
        Task LogoutAccount();
    }
}
