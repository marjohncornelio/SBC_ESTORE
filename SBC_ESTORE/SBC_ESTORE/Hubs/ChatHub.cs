using Microsoft.AspNetCore.SignalR;
using SBC_ESTORE.Data;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.ChatMessageService;
using SBC_ESTORE.Shared.DTO.ChatMessage;
using SBC_ESTORE.Shared.DTO.User;
using SBC_ESTORE.Shared.Enum;

namespace SBC_ESTORE.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DataContext context;
        private readonly IChatMessageService chatMessageService;
        public ChatHub(DataContext context, IChatMessageService chatMessageService)
        {
            this.context = context;
            this.chatMessageService = chatMessageService;
        }

        public async Task SendMessage(int userId, string message)
        {
            var user = await context.Users.FindAsync(userId);
            var chatMessage = new ChatMessage { User = user, Content = message, TimeStamp = DateTime.Now, UserId = userId };
            context.ChatMessages.Add(chatMessage);
            await context.SaveChangesAsync();

            if (user != null)
            {
                var userInfoDTO = new UserSideBarDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName,
                    Role = (AccountRoles)user.Role!,
                    AvatarURL = user.AvatarURL,
                };

                var addedMessage = new ChatMessageDTO()
                {
                    User = userInfoDTO,
                    Content = message,
                    TimeStamp = DateTime.Now,
                    UserId = userId,
                };
                await Clients.All.SendAsync("ReceiveMessage", addedMessage);
            }
        }
    }
}
