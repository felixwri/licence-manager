using LicenseeRecords.WebAPI.Models;
using LicenseeRecords.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseeRecords.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IJSONService _JSONService;
        private readonly string _filePath = Path.Combine("Data", "ActiveData", "Accounts.json");

        public AccountsController(ILogger<AccountsController> logger, IJSONService JSONService)
        {
            this._logger = logger;
            this._JSONService = JSONService;
        }

        private List<Account> GetAccounts() {
            _logger.LogInformation("Getting accounts from " + _filePath);

            List<Account> accounts = _JSONService.ReadFile<List<Account>>(_filePath);

            if (accounts == null)
            {
                return [];
            }

            return accounts;
        }

        private int generateId(List<Account> accounts)
        {
            int newId = 1;
            var existingIds = accounts.Select(a => a.AccountId).ToHashSet();
            while (existingIds.Contains(newId))
            {
                newId++;
            }
            return newId;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting Account Data");

            List<Account> accounts = this.GetAccounts();

            if (accounts.Count == 0)
            {
                return NotFound("No accounts found.");
            }

            return Ok(accounts);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account newAccount)
        {
            List<Account> accounts = this.GetAccounts();

            _logger.LogInformation("Adding account: " + newAccount.AccountName);

            newAccount.AccountId = generateId(accounts);

            accounts.Add(newAccount);

            _JSONService.WriteToFile(_filePath, accounts);

            return Ok("Success");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Account updatedAccount)
        {
            List<Account> accounts = this.GetAccounts();

            _logger.LogInformation("Updating account: " + updatedAccount.AccountName);

            int index = accounts.FindIndex(a => a.AccountId == id);

            if (index == -1)
            {
                return NotFound("Account not found");
            }
            updatedAccount.AccountId = id;
            accounts[index] = updatedAccount;

            _JSONService.WriteToFile(_filePath, accounts);

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            List<Account> accounts = this.GetAccounts();

            int index = accounts.FindIndex(a => a.AccountId == id);

            if (index == -1)
            {
                return NotFound("Account not found");
            }
            _logger.LogInformation("Deleting account: " + accounts[index].AccountName);

            accounts.RemoveAt(index);

            _JSONService.WriteToFile(_filePath, accounts);

            return Ok("Success");
        }
    }
}
