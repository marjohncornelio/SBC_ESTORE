using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Order;

namespace SBC_ESTORE.Client.Services.OrderServices
{
    public interface IClientOrderService
    {
        Task AddOrderFromCart(HashSet<CartItemDTO> selectedItems, int UserId, float Total);
        Task<List<OrderDTO>?> GetUserOrder(int userId);
        Task AddDirectOrder(CartItemDTO cartItem, int UserId, float Total);

        //Admin
        Task<List<OrderDTO>?> GetAllCustomerOrders();

    }
}
