using Microsoft.EntityFrameworkCore;
using SBC_ESTORE.Data;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.UserService;
using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Category;
using SBC_ESTORE.Shared.DTO.Order;
using SBC_ESTORE.Shared.DTO.Product;
using SBC_ESTORE.Shared.DTO.User;
using System.Net;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly DataContext context;
        public OrderService(DataContext context, IUserService userService)
        {
            this.context = context;
        }

        public async Task<GeneralResponse> AddOrderFromCart(OrderDTO order)
        {
            if (order == null)
                return new GeneralResponse("Order is Empty", HttpStatusCode.BadRequest);

            var user = await context.Users.FindAsync(order.UserId);

            var newOrder = new Order()
            {
                OrderItems = new List<OrderItem>(),
                Total = order.Total,
                UserId = order.UserId,
                User = user,
            };

            foreach (var item in order.OrderItems)
            {
                var product = await context.Products.FindAsync(item.ProductId);
                var orderItem = new OrderItem()
                {
                    OrderId = newOrder.Id,
                    Order = newOrder,
                    Product = product,
                    ProductId  = item.ProductId,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal,
                };

                newOrder.OrderItems.Add(orderItem);
                context.OrderItems.Add(orderItem);
            }

            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();
            return new GeneralResponse("Order Successfully Added");
        }

        public async Task<DataResponse<List<OrderDTO>>> GetAllUserOrder(int userId)
        {
            var orders = await context.Orders
                                        .Where(order => order.UserId == userId)
                                        .Include(order => order.OrderItems)
                                        .ThenInclude(orderItem => orderItem.Product)
                                        .ToListAsync();

            if (orders == null)
                return new DataResponse<List<OrderDTO>>(null!, "No Data Found", HttpStatusCode.NotFound);


            var userOrder = orders.Select(items => new OrderDTO
            {
                Id = items.Id,
                UserId = items.UserId,
                DateOrdered = items.DateOrdered,
                Total = items.Total,
                Status = items.Status,
                OrderItems = items.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal,
                    Product = new ProductDTO
                    {
                        Id = item.Product.Id,
                        Description = item.Product.Description,
                        ImageUrl = item.Product.ImageUrl,
                        Quantity = item.Product.Quantity,
                        Price = item.Product.Price,
                        Name = item.Product.Name,
                    }
                }).ToList()
            }).ToList();

            return new DataResponse<List<OrderDTO>>(userOrder!, "Products Fetched");
        }

        //Admin
        public async Task<DataResponse<List<OrderDTO>>> GetAllCustomerOrder()
        {
            var orders = await context.Orders
                                        .Include(order => order.User)
                                        .Include(order => order.OrderItems)
                                        .ThenInclude(orderItem => orderItem.Product)
                                        .ToListAsync();

            if (orders == null)
                return new DataResponse<List<OrderDTO>>(null!, "No Data Found", HttpStatusCode.NotFound);


            var userOrder = orders.Select(items => new OrderDTO
            {
                Id = items.Id,
                UserId = items.UserId,
                User = new UserDetailsDTO
                {
                    Id = items.User.Id,
                    Name = items.User.Name,
                    Email = items.User.Email,
                    Role = items.User.Role,
                    AvatarURL = items.User.AvatarURL,
                    UserName = items.User.UserName,
                    PhoneNum = items.User.PhoneNum,
                    Address = items.User.Address,
                },
                DateOrdered = items.DateOrdered,
                Total = items.Total,
                Status = items.Status,
                OrderItems = items.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal,
                    Product = new ProductDTO
                    {
                        Id = item.Product.Id,
                        Description = item.Product.Description,
                        ImageUrl = item.Product.ImageUrl,
                        Quantity = item.Product.Quantity,
                        Price = item.Product.Price,
                        Name = item.Product.Name,
                    }
                }).ToList()
            }).ToList();

            return new DataResponse<List<OrderDTO>>(userOrder!, "Products Fetched");
        }

    }
}
