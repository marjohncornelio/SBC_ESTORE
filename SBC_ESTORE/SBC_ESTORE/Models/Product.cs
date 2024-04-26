using SBC_ESTORE.Shared.DTO.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBC_ESTORE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category ProductCategory { get; set; }
        public List<CartItem>? CartItem { get; set; }
        public List<OrderItem>? OrderItem { get; set; }

    }
}
