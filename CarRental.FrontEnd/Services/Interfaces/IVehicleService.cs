using CarRental.FrontEnd.Models;

namespace CarRental.FrontEnd.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleModel>> GetAllVehiclesAsync();
        Task<VehicleModel> AddVehiclesAsync(VehicleModel newVehicle);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
