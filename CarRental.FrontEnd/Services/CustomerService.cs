using CarRental.FrontEnd.Models;
using CarRental.FrontEnd.Options;
using CarRental.FrontEnd.Services.Interfaces;
using System.Text.Json;
using System.Net.Http.Json;

namespace CarRental.FrontEnd.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public CustomerService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<CustomerModel> AddCustomersAsync(CustomerModel newCustomer)
        {
            var content = JsonContent.Create(newCustomer);

            var customerResponse = await _httpClient.PostAsync($"{_appSettings.BackendApiUrl}/customer/NewCustomer", content);

            if (!customerResponse.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await customerResponse.Content.ReadAsStringAsync();
            var customer = JsonSerializer.Deserialize<CustomerModel>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return customer;
        }

        public async Task<bool> DeleteCustomersAsync(int id)
        {
            var customerResponse = await _httpClient.DeleteAsync($"{_appSettings.BackendApiUrl}/customer/RemoveCustomer{id}");
            return customerResponse.IsSuccessStatusCode;
        }

        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            var customersResponse = await _httpClient.GetAsync($"{_appSettings.BackendApiUrl}/customer/Customers");
            if (!customersResponse.IsSuccessStatusCode)
            {
                return new List<CustomerModel>();
            }
            var result = await customersResponse.Content.ReadAsStringAsync();
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return customers;
        }

        public async Task<CustomerModel> UpdateCustomersAsync(int id, CustomerModel updateCustomer)
        {
            var content = JsonContent.Create(updateCustomer);
            var customerResponse = await _httpClient.PutAsync($"{_appSettings.BackendApiUrl}/customer/ModifyCustomer{id}", content);
            if (!customerResponse.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await customerResponse.Content.ReadAsStringAsync();
            var customer = JsonSerializer.Deserialize<CustomerModel>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return customer;
        }
    }
}
