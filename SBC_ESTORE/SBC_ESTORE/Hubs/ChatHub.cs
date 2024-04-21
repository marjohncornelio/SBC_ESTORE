using Microsoft.AspNetCore.SignalR;
using SBC_ESTORE.Data;

namespace SBC_ESTORE.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DataContext context;
        public ChatHub(DataContext context)
        {
            this.context = context;
        }

        public async Task SendMessage(int userId, string message)
        {
            //var user = await context.Users.FindAsync(userId);
            //var chatMessage = new ChatMessage { User = user, Message = message, DateTimeStamp = DateTime.Now };
            //context.ChatMessages.Add(chatMessage);
            //await context.SaveChangesAsync();

            //var addedMessage = new ChatMessageDTO()
            //{
            //    SenderUsername = user.UserName,
            //    SenderAvatar = user.AvatarURL,
            //    SenderId = user.Id,
            //    Message = chatMessage.Message,
            //    DateTimeStamp = chatMessage.DateTimeStamp
            //};


            await Clients.All.SendAsync("ReceiveMessage", addedMessage);
        }
    }
}
