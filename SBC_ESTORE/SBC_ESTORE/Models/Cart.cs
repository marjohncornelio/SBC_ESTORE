using SBC_ESTORE.Shared.DTO.Cart;

namespace SBC_ESTORE.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> CartItem { get; set; }
    }
}
