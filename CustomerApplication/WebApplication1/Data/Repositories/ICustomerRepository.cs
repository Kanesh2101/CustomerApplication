using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using static CustomerApplication.GlobalResponse.GlobalResponse;

namespace CustomerApplication.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerResponse> AddCustomer(Customer customer);
        Task<CustomerResponse> DeleteCustomer(string email);
        Task<ActionResult<IEnumerable<Customer>>> GetCustomer();
        Task<CustomerResponse> GetCustomer(string email);
        Task<CustomerResponse> UpdateCustomer(Customer customer);
    }
}