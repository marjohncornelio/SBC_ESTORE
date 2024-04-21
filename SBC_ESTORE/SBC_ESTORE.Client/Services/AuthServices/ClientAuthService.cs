using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using SBC_ESTORE.Shared.DTO.User;
using System.Net.Http.Json;

namespace SBC_ESTORE.Client.Services.AuthServices
{
    public class ClientAuthService : IClientAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ISnackbar snackbar;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly NavigationManager navigationManager;

        public ClientAuthService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.snackbar = snackbar;
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
            this.navigationManager = navigationManager;
        }

        public async Task<string?> LoginAccount(LoginDTO user)
        {
            var response = await httpClient.PostAsJsonAsync("api/auth/login", user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                snackbar.Add("Login Successfully", Severity.Success);
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(result);
                navigationManager.NavigateTo("/");
                return null;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return errorMessage;
            }
        }

        public async Task<string?> RegisterAccount(UserDTO user)
        {
            var response = await httpClient.PostAsJsonAsync("api/auth/register", user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                snackbar.Add("Registered Successfully", Severity.Success);
                navigationManager.NavigateTo("/login");
                return null;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return errorMessage;
            }
        }

        public async Task LogoutAccount()
        {
            await localStorageService.RemoveItemAsync("token");
            navigationManager.NavigateTo("/login");
        }
    }
}
