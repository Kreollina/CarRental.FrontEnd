using Blazored.LocalStorage;
using CarRental.FrontEnd.Authentication;
using CarRental.FrontEnd.Options;
using CarRental.FrontEnd.Services.Interfaces;
using CarRental.FrontEnd.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace CarRental.FrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Configuration.AddJsonFile("appsettings.json");

            var settings = new AppSettings();
            builder.Configuration.Bind(settings);

            builder.Services.AddMudServices();

            builder.Services.AddSingleton(settings);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddTransient<AuthTokenHandler>();

            builder.Services.AddHttpClient<CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();

            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            builder.Services.AddHttpClient<ICustomerService, CustomerService>().AddHttpMessageHandler<AuthTokenHandler>();
            builder.Services.AddSingleton<IReservationService, ReservationService>();
            builder.Services.AddHttpClient<IReservationService, ReservationService>().AddHttpMessageHandler<AuthTokenHandler>();

            await builder.Build().RunAsync();
        }
    }
}
