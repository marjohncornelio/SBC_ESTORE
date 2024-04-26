using SBC_ESTORE.Shared.DTO.ChatMessage;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.ChatMessageService
{
    public interface IChatMessageService
    {
        Task<DataResponse<List<ChatMessageDTO>>> GetAllChatMessage();
        Task<GeneralResponse> AddChatMessage(int userId, string message);
    }
}
