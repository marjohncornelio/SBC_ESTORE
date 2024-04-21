using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SBC_ESTORE.Client.Authentication.AuthHelpers;
using System.Security.Claims;

namespace SBC_ESTORE.Authentication
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public CustomAuthentication(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string? stringToken = await localStorageService.GetItemAsStringAsync("token");

                if (string.IsNullOrWhiteSpace(stringToken))

                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claims = AuthUtility.GetClaimsFromToken(stringToken);
                var claimsPrincipal = AuthUtility.SetClaimPrincipal(claims);
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
