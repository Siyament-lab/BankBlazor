using BankBlazor.API.Data;
using BankBlazor.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankBlazorContext _context;
        public AccountController ( BankBlazorContext context )
        {
            _context = context;
        }
        // get account balance
        [HttpGet ("{id}/balance")]
        public async Task<ActionResult<decimal>> GetAccountBalance ( int id )
        {
            var account = await _context.Accounts.FindAsync (id);

            if (account == null)
                return NotFound ();

            return Ok (account.Balance);
        }

        //Get: account by customer id
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountsByCustomer ( int customerId )
        {
            var accounts = await _context.Dispositions
                 .Where (d => d.CustomerId == customerId)
                 .Select (d => d.Account)
                 .ToListAsync ();
            if (!accounts.Any ())
            {
                return NotFound ();
            }
            return Ok (accounts);
        }
    }
}
