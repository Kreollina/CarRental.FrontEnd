using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace CarRental.FrontEnd.Authentication
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        public AuthTokenHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authTokenKey");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
