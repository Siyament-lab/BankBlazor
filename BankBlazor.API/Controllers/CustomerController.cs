using BankBlazor.API.Data;
using BankBlazor.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BankBlazorContext _context;
        public CustomerController ( BankBlazorContext context )
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers ()
        {
            return await _context.Customers.ToListAsync ();
        }
        //Get: customer by id
        [HttpGet ("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer ( int id )
        {
            var customer = await _context.Customers
                .Include (c => c.Dispositions)
                .ThenInclude (d => d.Account)
                .FirstOrDefaultAsync (c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound ();
            }
            return customer;
        }
    }
}
