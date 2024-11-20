using LicenseeRecords.Web.Services;
using LicenseeRecords.Web.Enums;
using LicenseeRecords.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseeRecords.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IProductsService _productsService;
        public AccountsController(IAccountsService accountsService, IProductsService productsService) {
            _accountsService = accountsService;
            _productsService = productsService;
        }
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountsService.GetAccountsAsync();

            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _accountsService.GetAccountByIdAsync(id);
            var products = await _productsService.GetProductsAsync();

            if (account == null)
            {
                return NotFound();
            }

            ViewBag.Products = products.ToList();

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
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await _productsService.GetProductsAsync();

            ViewBag.Products = products.ToList();

            Account account = new()
            {
                AccountId = 0,
                AccountName = "New Account",
                AccountStatus = Status.Active,
                ProductLicence = []
            };

            return View("Edit", account);
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
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accountsService.DeleteAccountAsync(account.AccountId);

                return RedirectToAction("Success");
            }
            // If the model state is invalid, return the view with the model to show validation errors
            return RedirectToAction("Error");
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
