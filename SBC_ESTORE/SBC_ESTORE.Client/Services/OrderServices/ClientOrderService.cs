using MudBlazor;
using SBC_ESTORE.Client.Services.UserService;
using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Order;
using System.Net.Http;
using System.Net.Http.Json;
using static MudBlazor.Colors;

namespace SBC_ESTORE.Client.Services.OrderServices
{
    public class ClientOrderService : IClientOrderService
    {
        private readonly HttpClient httpClient;
        private readonly ISnackbar snackbar;
        private readonly IClientUserService userService;

        public ClientOrderService(HttpClient httpClient, ISnackbar snackbar, IClientUserService userService)
        {
            this.httpClient = httpClient;
            this.snackbar = snackbar;
            this.userService = userService;
        }

        public async Task AddOrderFromCart(HashSet<CartItemDTO> selectedItems, int UserId, float Total)
        {
            try
            {
                OrderDTO order = new OrderDTO();

                var user = await userService.GetUserDetails(UserId);
                if (user != null)
                {
                    order = new OrderDTO()
                    {
                        OrderItems = new List<OrderItemDTO>(),
                        UserId = user.Id,
                        User = user,
                        Total = Total,
                    };

                    foreach (var item in selectedItems)
                    {
                        var orderItem = new OrderItemDTO()
                        {
                            OrderId = order.Id,
                            Product = item.Product,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Subtotal = item.Subtotal,
                        };

                        order.OrderItems.Add(orderItem);
                    }



                    var response = await httpClient.PostAsJsonAsync("api/order", order);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var updateCart = await httpClient.PostAsJsonAsync("api/cart/update-cart", selectedItems.ToList());
                        snackbar.Add(result, Severity.Success);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        snackbar.Add(result, Severity.Error);
                    }
                }
                else
                {
                    snackbar.Add("An error occurred: Please Try Again.", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                snackbar.Add("An error occurred: " + ex.Message, Severity.Error);
            }
        }

        public async Task AddDirectOrder(CartItemDTO cartItem, int UserId, float Total)
        {
            try
            {
                OrderDTO order = new OrderDTO();

                var user = await userService.GetUserDetails(UserId);
                if (user != null)
                {
                    order = new OrderDTO()
                    {
                        OrderItems = new List<OrderItemDTO>(),
                        UserId = user.Id,
                        User = user,
                        Total = Total,
                    };

                    var orderItem = new OrderItemDTO()
                    {
                        OrderId = order.Id,
                        Product = cartItem.Product,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Subtotal = cartItem.Subtotal,
                    };

                    order.OrderItems.Add(orderItem);

                    var response = await httpClient.PostAsJsonAsync("api/order", order);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        snackbar.Add(result, Severity.Success);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        snackbar.Add(result, Severity.Error);
                    }
                }
                else
                {
                    snackbar.Add("An error occurred: Please Try Again.", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                snackbar.Add("An error occurred: " + ex.Message, Severity.Error);
            }
        }

        public async Task<List<OrderDTO>?> GetUserOrder(int userId)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<OrderDTO>>($"api/order/{userId}");
                if (response != null)
                {
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                snackbar.Add("An error occurred: " + ex.Message, Severity.Error);
                return null;
            }
        }


        //Admin
        public async Task<List<OrderDTO>?> GetAllCustomerOrders()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<OrderDTO>>($"api/order/admin");
                if (response != null)
                {
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                snackbar.Add("An error occurred: " + ex.Message, Severity.Error);
                return null;
            }
        }

    }
}
