using Microsoft.EntityFrameworkCore;
using SBC_ESTORE.Client.Pages;
using SBC_ESTORE.Data;
using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Product;
using SBC_ESTORE.Shared.DTO.User;
using SBC_ESTORE.Shared.Enum;
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
            var getUser = await context.Users
                          .Include(x => x.Cart)
                          .FirstOrDefaultAsync(x => x.Id == Id);

            if (getUser == null)
                return new DataResponse<UserSideBarDTO>(null!, "User Not Found", HttpStatusCode.NotFound);

            var userCart = new CartDTO()
            {
                Id = getUser.Cart!.Id,
                UserId = getUser.Id
            };

            var user = new UserSideBarDTO()
            {
                Id = getUser.Id,
                Name = getUser.Name,
                UserName = getUser.UserName,
                AvatarURL = getUser.AvatarURL!,
                Role = (Shared.Enum.AccountRoles)getUser.Role!,
                Cart = userCart,
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

        public async Task<DataResponse<UserDetailsDTO>> GetUserDetails(int Id)
        {
            var getUser = await context.Users.FirstOrDefaultAsync(x => x.Id == Id);

            if (getUser == null)
                return new DataResponse<UserDetailsDTO>(null!, "User Not Found", HttpStatusCode.NotFound);

            var user = new UserDetailsDTO()
            {
                Id = getUser.Id,
                Name = getUser.Name,
                Email = getUser.Email,
                Role = (AccountRoles)getUser.Role!,
                AvatarURL = getUser.AvatarURL,
                UserName = getUser.UserName,
                PhoneNum = getUser.PhoneNum,
                Address = getUser.Address,
                PasswordHash = getUser.PasswordHash,
            };
            return new DataResponse<UserDetailsDTO>(user, "User Fetched");
        }

        public async Task<GeneralResponse> UpdateUserDetails(UserDetailsDTO user, int Id)
        {
            var getUser = await context.Users.FindAsync(Id);
            if (getUser == null)
                return new GeneralResponse("No User Found", HttpStatusCode.NotFound);

            getUser.Name = user.Name;
            getUser.Email = user.Email;
            getUser.Role = user.Role;
            getUser.AvatarURL = user.AvatarURL;
            getUser.UserName = user.UserName;
            getUser.PhoneNum = user.PhoneNum;
            getUser.Address = user.Address;

            context.Users.Update(getUser);
            await context.SaveChangesAsync();
            return new GeneralResponse("User Details Updated Successfully", HttpStatusCode.OK);
        }

        public async Task<GeneralResponse> UpdatePassword(ChangePasswordDTO password, int Id)
        {
            var getUser = await context.Users.FindAsync(Id);
            if (getUser == null)
                return new GeneralResponse("No User Found", HttpStatusCode.NotFound);



            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password.Password);
            getUser.PasswordHash = passwordHash;

            context.Users.Update(getUser);
            await context.SaveChangesAsync();
            return new GeneralResponse("Password Updated Successfully", HttpStatusCode.OK);
        }


        //Admin
        public async Task<DataResponse<List<UserDetailsDTO>>> GetAllUsers()
        {
            var allUsers = await context.Users.ToListAsync();
            if(allUsers == null)
                return new DataResponse<List<UserDetailsDTO>>(null!,"No User Found", HttpStatusCode.NotFound);

            var users = allUsers.Select(p => new UserDetailsDTO
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                UserName = p.Name,
                PhoneNum = p.PhoneNum,
                Address = p.Address,
                Role = p.Role,
                AvatarURL = p.AvatarURL,

            }).ToList();

            return new DataResponse<List<UserDetailsDTO>>(users, "Users Fetched");

        }

    }
}
