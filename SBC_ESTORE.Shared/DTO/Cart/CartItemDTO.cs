using SBC_ESTORE.Shared.DTO.Product;

namespace SBC_ESTORE.Shared.DTO.Cart
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public CartDTO? Cart { get; set; }
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int Quantity { get; set; }
        public float Subtotal { get; set; }


    }
}
