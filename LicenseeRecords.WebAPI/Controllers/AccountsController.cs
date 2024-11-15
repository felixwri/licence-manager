using LicenseeRecords.WebAPI.Models;
using LicenseeRecords.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicenseeRecords.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IJSONService _JSONService;

        public AccountsController(ILogger<AccountsController> logger, IJSONService JSONService)
        {
            this._logger = logger;
            this._JSONService = JSONService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Data", "Accounts.json");
            _logger.LogInformation("Getting accounts from " + filePath);
            
            List<Account> accounts = _JSONService.Read<List<Account>>(filePath);

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
