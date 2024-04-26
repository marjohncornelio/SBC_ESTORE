using SBC_ESTORE.Shared.DTO.ChatMessage;

namespace SBC_ESTORE.Client.Services.ChatMessageServices
{
    public interface IClientChatMessageService
    {
        Task<List<ChatMessageDTO>?> GetAllMessages();
    }
}
