using Microsoft.AspNetCore.Components;
using MudBlazor;
using SBC_ESTORE.Shared.DTO.Cart;
using SBC_ESTORE.Shared.DTO.Product;
using System.Net.Http;
using System.Net.Http.Json;

namespace SBC_ESTORE.Client.Services.CartServces
{
    public class ClientCartService : IClientCartService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;
        private readonly ISnackbar snackbar;

        public ClientCartService(HttpClient httpClient, NavigationManager navigationManager, ISnackbar snackbar)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
            this.snackbar = snackbar;
        }


        public async Task AddCartItemToCart(CartItemDTO cartItem)
        {
            try
            {

                var response = await httpClient.PostAsJsonAsync("api/cart/addtocart", cartItem);
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
            catch (Exception ex)
            {
                snackbar.Add("An error occurred: " + ex.Message, Severity.Error);
            }
        }
        public async Task<List<CartItemDTO>?> GetItemsFromCart(int Id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<CartItemDTO>>($"api/cart/{Id}");
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
