using Microsoft.AspNetCore.Components;
using MudBlazor;
using SBC_ESTORE.Shared.DTO.Product;
using System.Net.Http.Json;

namespace SBC_ESTORE.Client.Services.ProductServices
{
    public class ClientProductService : IClientProductService
    {
        private readonly HttpClient httpClient;
        private readonly ISnackbar snackbar;
        private readonly NavigationManager navigationManager;

        public ClientProductService(HttpClient httpClient, ISnackbar snackbar, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.snackbar = snackbar;
            this.navigationManager = navigationManager;
        }

        public async Task AddProduct(ProductDTO Product)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/admin/product", Product);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    snackbar.Add(result, Severity.Success);
                    navigationManager.NavigateTo("/admin/products");
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

        public async Task DeleteProduct(int Id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/admin/product/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    snackbar.Add(result, Severity.Success);
                    navigationManager.NavigateTo("/admin/products");
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

        public async Task<List<ProductDTO>?> GetAllProducts()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<ProductDTO>>("api/admin/product");
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

        public async Task<ProductDTO?> GetProductById(int Id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ProductDTO>($"api/admin/product/{Id}");
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

        public async Task UpdateProduct(ProductDTO Product, int Id)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/admin/product/{Id}", Product);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    snackbar.Add(result, Severity.Success);
                    navigationManager.NavigateTo("/admin/products");
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
    }
}
