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

        public async Task<CustomerResponse> GetCustomer(string email)
        {
            try
            {
                var customer = await _dbContext.Customer.FindAsync(email);

                if (customer == null)
                {
                    return new CustomerResponse(false, customer, "Customer Not Found");
                }

                return new CustomerResponse(true, customer, "Customer Created");
            }
            catch (Exception ex)
            {
                return new CustomerResponse(true, null, "Error at Get Customer By Email");

            }

        }


        public async Task<CustomerResponse> UpdateCustomer(Customer customer)
        {
            var existingCustomer = CustomerExists(customer.Email);

            if (!existingCustomer)
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
                if (!CustomerExists(customer.Email))
                {
                    return new CustomerResponse(false, null, "Customer Not Found");
                }
                else
                {
                    return new CustomerResponse(false, null, "Update Failed ");
                }
            }

            var result = _dbContext.Customer.Find(customer.Email);

            return new CustomerResponse(true, result, "Success");
        }


        public async Task<CustomerResponse> AddCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = CustomerExists(customer.Email);
                if (existingCustomer)
                {
                    return new CustomerResponse(false, null, "Duplicate Customer");
                }

                _dbContext.Customer.Add(customer);
                await _dbContext.SaveChangesAsync();

                return await GetCustomer(customer.Email);
            }
            catch (Exception ex) {
                return new CustomerResponse(false, null, "Create Customer Failed");
            }


        }


        public async Task<CustomerResponse> DeleteCustomer(string email)
        {
            try
            {
                var customer = await _dbContext.Customer.FindAsync(email);
                if (customer == null)
                {
                    return new CustomerResponse(false, null, "Customer Not Found");
                }

                _dbContext.Customer.Remove(customer);
                await _dbContext.SaveChangesAsync();

                return new CustomerResponse(true, null, "Customer Deleted");
            }
            catch (Exception ex) {
                return new CustomerResponse(false, null, "Delete Customer Failed");

            }

        }

        private bool CustomerExists(string email)
        {
            return _dbContext.Customer.Any(e => e.Email == email);
        }
    }
}
