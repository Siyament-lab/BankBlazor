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
        //Get account by id
        [HttpGet ("{id}")]
        public async Task<ActionResult<Account>> GetAccount ( int id )
        {
            var account = await _context.Accounts
                .Include (a => a.Transactions)
                .FirstOrDefaultAsync (a => a.AccountId == id);

            if (account == null)
                return NotFound ();

            return account;
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
