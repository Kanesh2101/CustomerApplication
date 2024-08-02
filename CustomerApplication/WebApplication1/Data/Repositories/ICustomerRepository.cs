using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<ActionResult<Customer>> AddCustomer(Customer customer);
        Task<ActionResult<bool>> DeleteCustomer(string email);
        Task<ActionResult<IEnumerable<Customer>>> GetCustomer();
        Task<ActionResult<Customer>> GetCustomer(string email);
        Task<GlobalResponse.GlobalResponse.CustomerResponse> UpdateCustomer(Customer customer);
    }
}