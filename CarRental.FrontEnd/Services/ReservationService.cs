using CarRental.FrontEnd.Models;
using CarRental.FrontEnd.Options;
using CarRental.FrontEnd.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarRental.FrontEnd.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public ReservationService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<ReservationModel> AddReservationAsync(ReservationModel newReservation)
        {
            var content = JsonContent.Create(newReservation);
            var reservationResponse = await _httpClient.PostAsync($"{_appSettings.BackendApiUrl}/reservation/NewReservation", content);
            if (!reservationResponse.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await reservationResponse.Content.ReadAsStringAsync();
            var reservation = JsonSerializer.Deserialize<ReservationModel>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return reservation;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservationResponse = await _httpClient.DeleteAsync($"{_appSettings.BackendApiUrl}/reservation/RemoveReservation{id}");
            return reservationResponse.IsSuccessStatusCode;
        }

        public async Task<List<ReservationModel>> GetReservationsAsync()
        {
            var reservationResponse = await _httpClient.GetAsync($"{_appSettings.BackendApiUrl}/reservation/AllReservations");
            if (!reservationResponse.IsSuccessStatusCode)
            {
                return new List<ReservationModel>();
            }
            var result = await reservationResponse.Content.ReadAsStringAsync();
            var reservations = JsonSerializer.Deserialize<List<ReservationModel>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return reservations;
        }

        public async Task<ReservationModel> UpdateReservationAsync(int id, ReservationModel updateReservation)
        {
            var content = JsonContent.Create(updateReservation);
            var reservationResponse = await _httpClient.PutAsync($"{_appSettings.BackendApiUrl}/reservation/ModifyReservation{id}", content);
            if (!reservationResponse.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await reservationResponse.Content.ReadAsStringAsync();
            var reservation = JsonSerializer.Deserialize<ReservationModel>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return reservation;
        }
    }
}