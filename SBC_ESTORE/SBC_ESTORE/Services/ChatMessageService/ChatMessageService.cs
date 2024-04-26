using Microsoft.EntityFrameworkCore;
using SBC_ESTORE.Data;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.ServiceResponse;
using SBC_ESTORE.Shared.DTO.ChatMessage;
using SBC_ESTORE.Shared.DTO.User;
using System.Net;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.ChatMessageService
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly DataContext context;

        public ChatMessageService(DataContext context)
        {
            this.context = context;
        }

        public async Task<GeneralResponse> AddChatMessage(int userId, string message)
        {
            try
            {
                var user = await context.Users.FindAsync(userId);
                var newMessage = new ChatMessage { User = user, Content = message, TimeStamp = DateTime.Now, UserId = userId };
                context.ChatMessages.Add(newMessage);
                await context.SaveChangesAsync();

                return new GeneralResponse("Message Sent");
            }
            catch
            {
                return new GeneralResponse("Error Occured", HttpStatusCode.BadRequest);
            }
     
        }

        public async Task<DataResponse<List<ChatMessageDTO>>> GetAllChatMessage()
        {
            var response = await context.ChatMessages
                .Include(cm => cm.User)
                .ToListAsync();

            var chatMessageDTOs = response.Select(cm => new ChatMessageDTO
            { 
                Id = cm.Id,
                Content = cm.Content,
                TimeStamp = cm.TimeStamp,
                User = new UserSideBarDTO  // Convert the User entity to UserDTO
                {
                    Id = cm.User!.Id,
                    UserName = cm.User.UserName,
                    AvatarURL = cm.User.AvatarURL
                }
            }).ToList();

            if (response != null)
                return new DataResponse<List<ChatMessageDTO>>(chatMessageDTOs, "Messages fetched");
            return new DataResponse<List<ChatMessageDTO>>(null!, "Error Occured... Please Try Again");

        }
    }
}
