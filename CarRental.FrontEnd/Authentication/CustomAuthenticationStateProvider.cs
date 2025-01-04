using Blazored.LocalStorage;
using CarRental.FrontEnd.Helpers;
using CarRental.FrontEnd.Models.Response;
using CarRental.FrontEnd.Options;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace CarRental.FrontEnd.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string AuthTokenKey = "authTokenKey";
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AppSettings _appSettings;

        public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _appSettings = appSettings;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await GetTokenAsync();
            return CreateAuthenticationState(token);
        }

        private async Task<string> GetTokenAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync(AuthTokenKey);
            return token ?? await Task.FromResult(string.Empty);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var authResponse = await _httpClient.PostAsync(
                $"{_appSettings.BackendApiUrl}/auth/login?username={username}&password={password}",
                new StringContent(string.Empty));
            if (authResponse.IsSuccessStatusCode)
            {
                var result = await authResponse.Content.ReadFromJsonAsync<TokenResponse>();
                await _localStorage.SetItemAsync(AuthTokenKey, result.Token);
                MarkUserAuthentication(result.Token);
                return true;
            }
            return false;
        }

        private void MarkUserAuthentication(string token)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(CreateAuthenticationState(token)));
        }

        private static AuthenticationState CreateAuthenticationState(string token)
        {
            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}
