using CarRental.FrontEnd.Models;

namespace CarRental.FrontEnd.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationModel>> GetReservationsAsync();
        Task<ReservationModel> AddReservationAsync(ReservationModel newReservation);
        Task<ReservationModel> UpdateReservationAsync(int id, ReservationModel updateReservation);
        Task<bool> DeleteReservationAsync(int id);
    }
}
