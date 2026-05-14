using BankBlazor.API.Data;
using BankBlazor.API.Entities;
using BankBlazor.API.Services;
using BankBlazor.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BankBlazorContext _context;
        private readonly TransactionValidationService _validationService;

        public TransactionController ( BankBlazorContext context, TransactionValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        // GET by account id
        [HttpGet ("account/{accountId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByAccount ( int accountId )
        {
            var transactions = await _context.Transactions
                .Where (t => t.AccountId == accountId)
                .OrderByDescending (t => t.Date)
                .ToListAsync ();

            if (!transactions.Any ())
                return NotFound ();

            return Ok (transactions);
        }
        // GET by customer id
        [HttpGet ("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByCustomer ( int customerId )
        {
            var transactions = await _context.Dispositions
                .Where (d => d.CustomerId == customerId)
                .SelectMany (d => d.Account.Transactions)
                .OrderByDescending (t => t.Date)
                .ToListAsync ();

            if (!transactions.Any ())
                return NotFound ();

            return Ok (transactions);
        }

        // POST, deposit
        [HttpPost ("deposit")]
        public async Task<ActionResult> Deposit ( [FromBody] DepositDto request )
        {
            var account = await _context.Accounts.FindAsync (request.AccountId);
            //Valideringar av belopp.
            var error = _validationService.ValidateAmount (request.Amount);
            if (error != null) return BadRequest (error);

            if (account == null)
                return NotFound ();

            account.Balance += request.Amount;

            _context.Transactions.Add (new Transaction
            {
                AccountId = request.AccountId,
                Date = DateOnly.FromDateTime (DateTime.Now),
                Type = "Credit",
                Operation = "Deposit",
                Amount = request.Amount,
                Balance = account.Balance
            });

            await _context.SaveChangesAsync ();
            return Ok (account.Balance);
        }

        // POST,withdraw
        [HttpPost ("Withdraw")]
        public async Task<ActionResult> Withdraw ( [FromBody] WithdrawDto request )
        {
            //Validering av belopp
            var error = _validationService.ValidateAmount (request.Amount);
            if (error != null) return BadRequest (error);

            //Validering av operation
            var operationError = _validationService.ValidateOperation (request.Operation);
            if (operationError != null) return BadRequest (operationError);

            var account = await _context.Accounts.FindAsync (request.AccountId);

            if (account == null)
                return NotFound ();

            if (account.Balance < request.Amount)
                return BadRequest ("Insufficient funds");

            account.Balance -= request.Amount;

            _context.Transactions.Add (new Transaction
            {
                AccountId = request.AccountId,
                Date = DateOnly.FromDateTime (DateTime.Now),
                Type = "Debit",
                Operation = request.Operation,
                Amount = -request.Amount,
                Balance = account.Balance
            });

            await _context.SaveChangesAsync ();
            return Ok (account.Balance);
        }

        // POST, transfer
        [HttpPost ("transfer")]
        public async Task<ActionResult> Transfer ( [FromBody] TransferPostDto request )
        {
            //Valideringar av belopp och konto för överföring.
            var amountError = _validationService.ValidateAmount (request.Amount);
            if (amountError != null) return BadRequest (amountError);
            var transferError = _validationService.ValidateTransfer (request.FromAccountId, request.ToAccountId);
            if (transferError != null) return BadRequest (transferError);

            var fromAccount = await _context.Accounts.FindAsync (request.FromAccountId);
            var toAccount = await _context.Accounts.FindAsync (request.ToAccountId);

            if (fromAccount == null || toAccount == null)
                return NotFound ();

            if (fromAccount.Balance < request.Amount)
                return BadRequest ("Insufficient funds");

            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;

            _context.Transactions.Add (new Transaction
            {
                AccountId = request.FromAccountId,
                Date = DateOnly.FromDateTime (DateTime.Now),
                Type = "Debit",
                Operation = $"Transfer to account {request.ToAccountId}",
                Amount = -request.Amount,
                Balance = fromAccount.Balance
            });

            _context.Transactions.Add (new Transaction
            {
                AccountId = request.ToAccountId,
                Date = DateOnly.FromDateTime (DateTime.Now),
                Type = "Credit",
                Operation = $"Transfer from account {request.FromAccountId}",
                Amount = request.Amount,
                Balance = toAccount.Balance
            });

            await _context.SaveChangesAsync ();
            return Ok ();
        }
    }
    //Kanske behöver skapa separata mappar och filer för DTOs.
    //i så fall tar vi bort de här & refaktorerar koden senare.
    //public class DepositWithdrawDto
    //{
    //    public int AccountId { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Operation { get; set; } = "Withdrawal in Cash";
    //}

    //public class TransferDto
    //{
    //    public int FromAccountId { get; set; }
    //    public int ToAccountId { get; set; }
    //    public decimal Amount { get; set; }
    //}
}