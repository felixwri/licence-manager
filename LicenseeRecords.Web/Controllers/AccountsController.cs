using LicenseeRecords.Web.Services;
using LicenseeRecords.WebAPI.Enums;
using LicenseeRecords.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseeRecords.Web.Controllers
{
    public class AccountsController : Controller
    {
        AccountsService _accountsService;
        public AccountsController(AccountsService accountsService) {
            _accountsService = accountsService;
        }
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountsService.GetAccountsAsync();
            Console.WriteLine(accounts.Count());

            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _accountsService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accountsService.UpdateAccountAsync(account.AccountId, account);

                return RedirectToAction("Success");
            }
            return RedirectToAction("Failed");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Account account = new()
            {
                AccountId = 0,
                AccountName = "New Account",
                AccountStatus = Status.Active,
                ProductLicence = []
            };

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accountsService.AddAccountAsync(account);

                return RedirectToAction("Success");
            }
            // If the model state is invalid, return the view with the model to show validation errors
            return RedirectToAction("Failed");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
