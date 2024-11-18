using LicenseeRecords.Web.Services;
using LicenseeRecords.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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
        public IActionResult Edit(Account model)
        {
            Console.WriteLine("Edit");
            Console.WriteLine(model.ToJson());

            if (ModelState.IsValid)
            {
          
                // Save the updated account details
                // ...

                //return RedirectToAction("Index");
            }

            // If the model state is invalid, return the view with the model to show validation errors
            return RedirectToAction("Index");
        }
    }
}
