using static SBC_ESTORE.Services.ServiceResponse.Response;
using SBC_ESTORE.Shared.DTO.User;
using Microsoft.IdentityModel.Tokens;
using SBC_ESTORE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SBC_ESTORE.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace SBC_ESTORE.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        public User user = new User();
        private readonly IConfiguration config;
        private readonly DataContext context;


        public AuthService(DataContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse("Model is empty", HttpStatusCode.BadRequest);

            var getExistingEmail = await context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if(getExistingEmail is not null) return new GeneralResponse("Email is already Registered", HttpStatusCode.BadRequest);

            var getExistingUsername = await context.Users.FirstOrDefaultAsync(u => u.UserName == userDTO.UserName);
            if (getExistingUsername is not null) return new GeneralResponse("Username already Registered, Please choose a different username.", HttpStatusCode.BadRequest);

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.PasswordHash = passwordHash;
            user.Role = userDTO.Role;
            user.AvatarURL = userDTO.AvatarURL;
            user.UserName = userDTO.UserName;
            user.PhoneNum = userDTO.PhoneNum;
            user.Address = userDTO.Address;

            context.Users.Add(user);
            var saveUser = await context.SaveChangesAsync();
            if (saveUser == 0)
                return new GeneralResponse("Error occured. Try again later...", HttpStatusCode.BadRequest);

            Cart UserCart = new Cart()
            {
                UserId = user.Id,
                User = user,
            };

            context.Carts.Add(UserCart);
            var saveUserCart = await context.SaveChangesAsync();
            if (saveUserCart == 0)
                return new GeneralResponse("Error occured. Try again later...", HttpStatusCode.BadRequest);

            return new GeneralResponse("User Added Succesfully", HttpStatusCode.OK);
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO is null) return new LoginResponse(null!, "Error occured, Login input is null", HttpStatusCode.BadRequest);

            var getUser = await context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.EmailOrUsername || u.UserName == loginDTO.EmailOrUsername);
            if (getUser is null)
                return new LoginResponse(null!, "User not found", HttpStatusCode.NotFound);

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.PasswordHash))
                return new LoginResponse(null!, "Password Incorrect", HttpStatusCode.BadRequest);

            string token = CreateTokenAsync(getUser);

            return new LoginResponse(token, "Login Successfully", HttpStatusCode.OK);
        }
        public string CreateTokenAsync(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()!),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(

                   claims: claims,
                   expires: DateTime.UtcNow.AddDays(1),
                   signingCredentials: credentials

                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
    }
}
