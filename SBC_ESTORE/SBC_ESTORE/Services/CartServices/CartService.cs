using Microsoft.EntityFrameworkCore;
using SBC_ESTORE.Data;
using SBC_ESTORE.Models;
using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Category;
using SBC_ESTORE.Shared.DTO.Product;
using System.Net;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.CartServices
{
    public class Cartservice : ICartService
    {
        private readonly DataContext context;

        public Cartservice(DataContext context)
        {
            this.context = context;
        }

        public async Task<GeneralResponse> AddCartItemToCart(CartItemDTO cartItem)
        {
            if (cartItem == null)
                return new GeneralResponse("Cart Item is empty", HttpStatusCode.BadRequest);

            var cart = await context.Carts
                .Include(ci => ci.CartItem)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == cartItem.CartId);

            if (cart == null)
                return new GeneralResponse("Cart not found", HttpStatusCode.NotFound);

            var product = await context.Products
                                        .Include(ci => ci.ProductCategory)
                                        .FirstOrDefaultAsync(i => i.Id == cartItem.ProductId);
            if (product == null)
                return new GeneralResponse("Product not found", HttpStatusCode.NotFound);

            var existingItem = cart.CartItem?.Find(items => cartItem.Product?.Id == items.Product?.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                existingItem.Subtotal += cartItem.Subtotal;
                await context.SaveChangesAsync();

                return new GeneralResponse("Product successfully added");
            }
            else
            {
                var newCartItem = new CartItem()
                {
                    CartId = cartItem.CartId,
                    Cart = cart,
                    ProductId = cartItem.ProductId,
                    Product = product,
                    Quantity = cartItem.Quantity,
                    Subtotal = cartItem.Subtotal,
                };

                context.CartItems.Add(newCartItem);
                await context.SaveChangesAsync();

                return new GeneralResponse("Product successfully added");
            }

        }

        public async Task<DataResponse<List<CartItemDTO>>> GetAllItemsFromCart(int userId)
        {
            var cart = await context.Carts
                .Include(c => c.CartItem)  
                    .ThenInclude(ci => ci.Product)
                    .ThenInclude(ci => ci.ProductCategory)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return new DataResponse<List<CartItemDTO>>(null!, "No Data Found", HttpStatusCode.NotFound);

            if(cart.CartItem == null)
                return new DataResponse<List<CartItemDTO>>(null!, "No Items From Cart", HttpStatusCode.NotFound);

            var cartItems = cart.CartItem?.Select(items => new CartItemDTO
            {
                Id = items.Id,
                CartId = items.CartId,
                ProductId = items.ProductId,
                Product = items.Product == null ? null : new ProductDTO
                {
                    Id = items.Product.Id,
                    Name = items.Product.Name,
                    Price = items.Product.Price,
                    ImageUrl = items.Product.ImageUrl,
                    Description = items.Product.Description,
                    Quantity = items.Product.Quantity,
                    CategoryId = items.Product.CategoryId,
                    ProductCategory = new CategoryDTO
                    { 
                        Id = items.Product.ProductCategory.Id,
                        Name = items.Product.ProductCategory.Name
                    },
                },
                Quantity = items.Quantity,
                Subtotal = items.Subtotal,
            }).ToList();

            return new DataResponse<List<CartItemDTO>>(cartItems!, "Products Fetched");
        }

        public async Task<GeneralResponse> UpdateCartItems(List<CartItemDTO> cartItems)
        {
            try
            {
                foreach (var item in cartItems)
                {
                    var itemToDelete = await context.CartItems.FindAsync(item.Id);
                    if(itemToDelete != null)
                        context.CartItems.Remove(itemToDelete);

                }

                await context.SaveChangesAsync();

                return new GeneralResponse("Cart Updated");
            }
            catch
            {
                return new GeneralResponse("Error Occurred", HttpStatusCode.BadRequest);
            }

        }
    }
}
