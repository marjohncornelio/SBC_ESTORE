using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SBC_ESTORE.Shared.DTO.User;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SBC_ESTORE.Shared.DTO.Product;

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

        public async Task UploadAvatar(string AvatarUrl, int Id)
        {
            var response = await httpClient.PostAsJsonAsync($"api/user/uploadAvatar/{Id}", AvatarUrl);
            if (response.IsSuccessStatusCode)
            { 
                var result = await response.Content.ReadAsStringAsync();
                    snackbar.Add(result, Severity.Success);
                return;
            }
            snackbar.Add("Error Occured, Try Again later", Severity.Error);
        }

        public async Task<UserDetailsDTO?> GetUserDetails(int Id)
        {
            var response = await httpClient.GetFromJsonAsync<UserDetailsDTO?>($"api/user/details/{Id}");
            if (response != null)
                return response;
            return null;
        }

        public async Task<string?> UpdateUserDetails(UserDetailsDTO user, int Id)
        {

            var response = await httpClient.PutAsJsonAsync($"api/user/details/{Id}", user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                snackbar.Add("Details Updated Successfully", Severity.Success);
                return null;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return errorMessage;
            }
        }

        public async Task<string?> ChangeUserPassword(ChangePasswordDTO password, int Id)
        {

            var response = await httpClient.PutAsJsonAsync($"api/user/change-password/{Id}", password);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                snackbar.Add("Password Updated Successfully", Severity.Success);
                return null;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return errorMessage;
            }
        }

        //Admin
        public async Task<List<UserDetailsDTO>?> GetAllUsers()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<UserDetailsDTO>>("api/user/admin");
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
