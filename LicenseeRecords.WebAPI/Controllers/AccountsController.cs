using LicenseeRecords.WebAPI.Models;
using LicenseeRecords.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

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
            List<Account> accounts = _JSONService.ReadFile<List<Account>>(_filePath);

            if (accounts == null) return [];
            
            return accounts;
        }

        private int GenerateId<T>(List<T> items, Func<T, int> idSelector)
        {
            int newId = 1;
            var existingIds = items.Select(idSelector).ToHashSet();
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

            List<Account> accounts = GetAccounts();

            if (accounts.Count == 0)
            {
                return NotFound("No accounts found.");
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            _logger.LogInformation("Getting Account With Id: " + id);

            List<Account> accounts = GetAccounts();

            Account? account = accounts.Find(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound("No accounts found.");
            }

            return Ok(account);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account newAccount)
        {
            List<Account> accounts = GetAccounts();

            _logger.LogInformation("Adding account: " + newAccount.AccountName);

            // Create a new account id for the account itself
            newAccount.AccountId = GenerateId(accounts, account => account.AccountId);

            // Iterate over all new licences and give them an id
            foreach (Licence licence in newAccount.ProductLicence)
            {
                licence.LicenceId = GenerateId(newAccount.ProductLicence, licence => licence.LicenceId);
            }

            accounts.Add(newAccount);

            _JSONService.WriteToFile(_filePath, accounts);

            return Ok("Success");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Account updatedAccount)
        {
            List<Account> accounts = GetAccounts();

            _logger.LogInformation("Updating account: " + updatedAccount.AccountName);

            int index = accounts.FindIndex(a => a.AccountId == id);

            if (index == -1)
            {
                return NotFound("Account not found");
            }
            // Iterate over all new licences and give them an id
            foreach (Licence licence in updatedAccount.ProductLicence)
            {
                if (licence.LicenceId == 0)
                {
                    licence.LicenceId = GenerateId(updatedAccount.ProductLicence, licence => licence.LicenceId);
                }
            }

            updatedAccount.AccountId = id;
            accounts[index] = updatedAccount;

            _JSONService.WriteToFile(_filePath, accounts);

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            List<Account> accounts = GetAccounts();

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
