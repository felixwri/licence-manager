using LicenseeRecords.Web.Services;
using LicenseeRecords.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseeRecords.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsService _productsService;
        private readonly AccountsService _accountsService;
        public ProductsController(ProductsService productsService, AccountsService accountsService)
        {
            _productsService = productsService;
            _accountsService = accountsService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productsService.GetProductsAsync();

            return View(products);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            var accounts = await _accountsService.GetAccountsAsync();

            var dependentAccounts = accounts
                .Where(account => account.ProductLicence.Any(licence => licence.Product.ProductId == id))
                .ToList();


            ViewBag.Dependencies = dependentAccounts;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.AddProductAsync(product);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Failed");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.UpdateProductAsync(product.ProductId, product);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Failed");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.DeleteProductAsync(product.ProductId);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Failed");
        }

        public IActionResult Failed()
        {
            return View();
        }
    }
}
