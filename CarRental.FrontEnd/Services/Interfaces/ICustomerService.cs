using CarRental.FrontEnd.Models;

namespace CarRental.FrontEnd.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetCustomersAsync();
        Task<CustomerModel> AddCustomersAsync(CustomerModel newCustomer);
        Task<CustomerModel> UpdateCustomersAsync(int id, CustomerModel updateCustomer);
        Task<bool> DeleteCustomersAsync(int id);
    }
}
