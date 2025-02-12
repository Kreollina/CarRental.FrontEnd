using CarRental.FrontEnd.Models;
using CarRental.FrontEnd.Options;
using CarRental.FrontEnd.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarRental.FrontEnd.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public VehicleService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<VehicleModel> AddVehiclesAsync(VehicleModel newVehicle)
        {
            var content = JsonContent.Create(newVehicle);
            var vehicleResponse = await _httpClient.PostAsync($"{_appSettings.BackendApiUrl}/vehicle/newvehicle", content);
            if(!vehicleResponse.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await vehicleResponse.Content.ReadAsStringAsync();
            var vehicle =JsonSerializer.Deserialize<VehicleModel>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return vehicle;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicleResponse = await _httpClient.DeleteAsync($"{_appSettings.BackendApiUrl}/vehicle/removevehicle{id}");
            return vehicleResponse.IsSuccessStatusCode;
        }

        public async Task<List<VehicleModel>> GetAllVehiclesAsync()
        {
            var vehicleResponse = await _httpClient.GetAsync($"{_appSettings.BackendApiUrl}/vehicle/vehicles");
            if(!vehicleResponse.IsSuccessStatusCode)
            {
                return new List<VehicleModel>();
            }
            var result = await vehicleResponse.Content.ReadAsStringAsync();
            var vehicles = JsonSerializer.Deserialize<List<VehicleModel>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return vehicles;
        }
    }
}
