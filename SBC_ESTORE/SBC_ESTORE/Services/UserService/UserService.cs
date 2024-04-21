using SBC_ESTORE.Data;
using SBC_ESTORE.Shared.DTO.User;
using System.Net;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }

        public async Task<DataResponse<UserSideBarDTO>> GetSideBarUserInfo(int Id)
        {
            var getUser = await context.Users.FindAsync(Id);
            if (getUser == null)
                return new DataResponse<UserSideBarDTO>(null!, "User Not Found", HttpStatusCode.NotFound);

            var user = new UserSideBarDTO()
            {
                Id = getUser.Id,
                Name = getUser.Name,
                UserName = getUser.UserName,
                AvatarURL = getUser.AvatarURL!,
                Role = (Shared.Enum.AccountRoles)getUser.Role!,
            };
            return new DataResponse<UserSideBarDTO>(user, "User Fetched");
        }
        public async Task<GeneralResponse> UploadAvatar(int Id, string AvatarUrl)
        {
            var getUser = await context.Users.FindAsync(Id);
            if (getUser == null)
                return new GeneralResponse("No User Found", HttpStatusCode.NotFound);

            getUser.AvatarURL = AvatarUrl;
            context.Users.Update(getUser);
            await context.SaveChangesAsync();
            return new GeneralResponse("Image Upload Successfully", HttpStatusCode.OK);
        }
    }
}
