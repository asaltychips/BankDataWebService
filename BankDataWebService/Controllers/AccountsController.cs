
using bankdatawebservice.Models;
using BankDataWebService.DTOs;
using BankDataWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDataWebService.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly TemporaryAccountStore _store;

        public AccountsController(TemporaryAccountStore store)
        {
            _store = store;
        }

        // 1. CREATE ACCOUNT
        [HttpPost]
        public IActionResult CreateAccount(
            [FromBody] CreateAccountDto request)
        {
            var account = new Account
            {
                Id = _store.NextId++,
                AccountNumber =
                    Guid.NewGuid().ToString("N")[..12],
                UserId = request.UserId,
                AccountType = request.AccountType,
                Balance = 0,
                IsActive = true
            };

            _store.Accounts.Add(account);

            return CreatedAtAction(
                nameof(GetAccount),
                new { accountNumber = account.AccountNumber },
                account);
        }

        // 2. RETRIEVE ACCOUNT
        [HttpGet("{accountNumber}")]
        public IActionResult GetAccount(string accountNumber)
        {
            var account = _store.Accounts
                .FirstOrDefault(a =>
                    a.AccountNumber == accountNumber);

            if (account == null)
            {
                return NotFound(new
                {
                    error = "ACCOUNT_NOT_FOUND",
                    message = "Account does not exist.",
                    details = (object?)null
                });
            }

            return Ok(account);
        }

        // 3. UPDATE ACCOUNT
        [HttpPut("{accountNumber}")]
        public IActionResult UpdateAccount(
            string accountNumber,
            [FromBody] UpdateAccountDto request)
        {
            var account = _store.Accounts
                .FirstOrDefault(a =>
                    a.AccountNumber == accountNumber);

            if (account == null)
            {
                return NotFound(new
                {
                    error = "ACCOUNT_NOT_FOUND",
                    message = "Account does not exist.",
                    details = (object?)null
                });
            }

            if (!account.IsActive)
            {
                return UnprocessableEntity(new
                {
                    error = "ACCOUNT_INACTIVE",
                    message = "Cannot update an inactive account.",
                    details = (object?)null
                });
            }

            account.AccountType = request.AccountType;

            return NoContent();
        }

        // 4. DEACTIVATE ACCOUNT
        [HttpPatch("{accountNumber}/deactivate")]
        public IActionResult DeactivateAccount(
            string accountNumber)
        {
            var account = _store.Accounts
                .FirstOrDefault(a =>
                    a.AccountNumber == accountNumber);

            if (account == null)
            {
                return NotFound(new
                {
                    error = "ACCOUNT_NOT_FOUND",
                    message = "Account does not exist.",
                    details = (object?)null
                });
            }

            account.IsActive = false;

            return NoContent();
        }
    }
}
