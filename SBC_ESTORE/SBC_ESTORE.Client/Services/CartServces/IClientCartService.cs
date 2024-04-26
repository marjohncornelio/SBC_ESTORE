using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Product;

namespace SBC_ESTORE.Client.Services.CartServces
{
    public interface IClientCartService
    {
        Task AddCartItemToCart(CartItemDTO cartItem);
        Task<List<CartItemDTO>?> GetItemsFromCart(int Id);
    }
}
