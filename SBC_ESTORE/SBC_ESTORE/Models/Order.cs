using SBC_ESTORE.Shared.Enum;

namespace SBC_ESTORE.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DateOrdered { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public float Total { get; set; }
        public OrderStatus? Status { get; set; } = OrderStatus.PENDING;
    }
}
