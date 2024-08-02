using CustomerApplication.Data;
using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CustomerApplication.GlobalResponse.GlobalResponse;

namespace CustomerApplication.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        public readonly CustomerDbContext _dbContext;

        public CustomerRepository(CustomerDbContext customerDbContext)
        {
            _dbContext = customerDbContext;
        }

        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            try
            {
                return await _dbContext.Customer.ToListAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            var customer = await _dbContext.Customer.FindAsync(email);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }


        public async Task<CustomerResponse> UpdateCustomer(Customer customer)
        {
            var existingCustomer = CustomerExists(customer.email);

            if (existingCustomer)
            {
                return new CustomerResponse(false, null, "Customer Not Found");
            }
            _dbContext.Customer.Update(customer);

            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.email))
                {
                    return new CustomerResponse(false, null, "Customer Not Found");
                }
                else
                {
                    return new CustomerResponse(false, null, "Update Failed ");
                }
            }

            var result = _dbContext.Customer.Find(customer.email);

            return new CustomerResponse(true, result, "Success");
        }


        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            var existingCustomer = CustomerExists(customer.email);
            if (existingCustomer)
            {
                return null;
            }

            _dbContext.Customer.Add(customer);
            await _dbContext.SaveChangesAsync();

            return await GetCustomer(customer.email);
        }


        public async Task<ActionResult<bool>> DeleteCustomer(string email)
        {
            var customer = await _dbContext.Customer.FindAsync(email);
            if (customer == null)
            {
                return null;
            }

            _dbContext.Customer.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool CustomerExists(string email)
        {
            return _dbContext.Customer.Any(e => e.email == email);
        }
    }
}
