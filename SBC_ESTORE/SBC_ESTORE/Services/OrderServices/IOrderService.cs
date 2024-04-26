using SBC_ESTORE.Shared.DTO.Order;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.OrderServices
{
    public interface IOrderService
    {
        Task<GeneralResponse> AddOrderFromCart(OrderDTO order);
        Task<DataResponse<List<OrderDTO>>> GetAllUserOrder(int userId);

        //Admin
        Task<DataResponse<List<OrderDTO>>> GetAllCustomerOrder();


    }
}
