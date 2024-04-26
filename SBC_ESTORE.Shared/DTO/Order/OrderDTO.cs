using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.User;
using SBC_ESTORE.Shared.Enum;

namespace SBC_ESTORE.Shared.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDetailsDTO? User { get; set; }
        public DateTime DateOrdered { get; set; } = DateTime.Now;
        public List<OrderItemDTO>? OrderItems { get; set; }
        public float Total { get; set; }
        public OrderStatus? Status { get; set; } = OrderStatus.PENDING;

    }
}
