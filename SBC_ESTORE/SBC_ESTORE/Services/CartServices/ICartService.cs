using SBC_ESTORE.Shared.DTO.Cart;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.CartServices
{
    public interface ICartService
    {
        Task<GeneralResponse> AddCartItemToCart(CartItemDTO cartItem);
        Task<DataResponse<List<CartItemDTO>>> GetAllItemsFromCart(int userId);

        Task<GeneralResponse> UpdateCartItems(List<CartItemDTO> cartItems);
    }
}
