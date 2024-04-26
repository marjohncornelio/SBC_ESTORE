using SBC_ESTORE.Shared.DTO.User;

namespace SBC_ESTORE.Shared.DTO.Cart
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public List<CartItemDTO>? CartItem { get; set; }
    }
}
