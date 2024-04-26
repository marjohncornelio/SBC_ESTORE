
using SBC_ESTORE.Shared.DTO.User;

namespace SBC_ESTORE.Shared.DTO.ChatMessage
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Content { get; set; }
        public int UserId { get; set; }
        public UserSideBarDTO? User { get; set; }
    }
}
