using LicenseeRecords.Web.Services;
using LicenseeRecords.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseeRecords.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IAccountsService _accountsService;
        public ProductsController(IProductsService productsService, IAccountsService accountsService)
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
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.UpdateProductAsync(product.ProductId, product);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.DeleteProductAsync(product.ProductId);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
