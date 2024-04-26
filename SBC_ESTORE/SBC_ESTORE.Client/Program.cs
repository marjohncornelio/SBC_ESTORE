using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using SBC_ESTORE.Client;
using SBC_ESTORE.Client.Services.AuthServices;
using SBC_ESTORE.Client.Services.CartServces;
using SBC_ESTORE.Client.Services.CategoryServices;
using SBC_ESTORE.Client.Services.ChatMessageServices;
using SBC_ESTORE.Client.Services.OrderServices;
using SBC_ESTORE.Client.Services.ProductServices;
using SBC_ESTORE.Client.Services.UserService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//Services
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<IClientUserService, ClientUserService>();
builder.Services.AddScoped<IClientProductService, ClientProductService>();
builder.Services.AddScoped<IClientCategoryService, ClientCategoryService>();
builder.Services.AddScoped<IClientChatMessageService, ClientChatMessageService>();
builder.Services.AddScoped<IClientCartService, ClientCartService>();
builder.Services.AddScoped<IClientOrderService, ClientOrderService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 1000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.PopoverOptions.ThrowOnDuplicateProvider = false;
});



await builder.Build().RunAsync();
