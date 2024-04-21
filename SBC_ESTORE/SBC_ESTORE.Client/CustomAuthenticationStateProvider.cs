using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SBC_ESTORE.Client.Authentication.AuthHelpers;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace SBC_ESTORE.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private readonly HttpClient httpClient;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            this.localStorageService = localStorageService;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string? stringToken = await localStorageService.GetItemAsStringAsync("token");

                if (string.IsNullOrEmpty(stringToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claims = AuthUtility.GetClaimsFromToken(stringToken);
                var claimsPrincipal = AuthUtility.SetClaimPrincipal(claims);
                httpClient.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue("Bearer", stringToken.Replace("\"", ""));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }

        }

        public async Task UpdateAuthenticationState(string? token)
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var userSession = AuthUtility.GetClaimsFromToken(token);
                claimsPrincipal = AuthUtility.SetClaimPrincipal(userSession);
                await localStorageService.SetItemAsStringAsync("token", token);
            }
            else
            {
                claimsPrincipal = anonymous;
                await localStorageService.RemoveItemAsync("token");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
