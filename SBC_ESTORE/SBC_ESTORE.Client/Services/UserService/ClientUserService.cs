using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SBC_ESTORE.Shared.DTO.User;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SBC_ESTORE.Client.Services.UserService
{
    public class ClientUserService : IClientUserService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly ISnackbar snackbar;

        public ClientUserService(HttpClient httpClient, ILocalStorageService localStorageService, ISnackbar snackbar)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.snackbar = snackbar;
        }

        public async Task<UserSideBarDTO?> GetUserInfoForSidebar(int id)
        {
            var response = await httpClient.GetFromJsonAsync<UserSideBarDTO?>($"api/user/{id}");
            if(response != null)
                return response;
            return null;
        }

        public async Task UploadAvatar(string AvatarUrl, int id)
        {
            var response = await httpClient.PostAsJsonAsync($"api/user/uploadAvatar/{id}", AvatarUrl);
            if (response.IsSuccessStatusCode)
            { 
                var result = await response.Content.ReadAsStringAsync();
                    snackbar.Add(result, Severity.Success);
                return;
            }
            snackbar.Add("Error Occured, Try Again later", Severity.Error);
        }
    }
}
