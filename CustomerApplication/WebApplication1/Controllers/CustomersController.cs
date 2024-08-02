using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Data;
using CustomerApplication.Models;
using CustomerApplication.Data.Repositories;

namespace CustomerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _customerRepository.GetCustomer();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            var customer = await _customerRepository.GetCustomer(email);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Customer customer)
        {
            var createdCustomer = await _customerRepository.UpdateCustomer(customer);

            return Ok(createdCustomer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var createdCustomer = await _customerRepository.AddCustomer(customer);

            return Ok(createdCustomer);

            //return CreatedAtAction("GetCustomer", new { id = customer.email}, customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string email) 
        {
            var customer = await _customerRepository.DeleteCustomer(email);
            return Ok(customer);
        }

      
    }
}
