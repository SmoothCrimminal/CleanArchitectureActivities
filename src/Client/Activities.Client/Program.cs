using Activities.Client;
using Activities.Client.Auth;
using Activities.Interfaces.Remote;
using Activities.Remote;
using Activities.Services.Account;
using Activities.Services.Activities;
using Activities.Services.Profiles;
using Blazored.LocalStorage;
using Cropper.Blazor.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7048") });
builder.Services.AddScoped<ActivitiesService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ProfilesService>();

builder.Services.AddScoped<IHttpResponseHandler, HttpResponseHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMudServices();
builder.Services.AddCropper();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
