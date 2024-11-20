using LicenseeRecords.WebAPI.Models;
using LicenseeRecords.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseeRecords.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IJSONService _JSONService;
        private readonly string _filePath = Path.Combine("Data", "ActiveData", "Products.json");

        public ProductsController(ILogger<ProductsController> logger, IJSONService JSONService)
        {
            this._logger = logger;
            this._JSONService = JSONService;
        }

        private List<Product> GetProducts()
        {
            List<Product> products = _JSONService.ReadFile<List<Product>>(_filePath);

            if (products == null) return [];

            return products;
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
            _logger.LogInformation("Getting Product Data");

            List<Product> products = GetProducts();

            if (products.Count == 0)
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            _logger.LogInformation("Getting Product With Id: " + id);

            List<Product> products = GetProducts();

            Product? product = products.Find(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound("No product with that Id found.");
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product newProduct)
        {
            List<Product> products = GetProducts();

            _logger.LogInformation("Adding product: " + newProduct.ProductName);

            // Create a new product id
            newProduct.ProductId = GenerateId(products, product => product.ProductId);

            products.Add(newProduct);

            _JSONService.WriteToFile(_filePath, products);

            return Ok("Success");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            List<Product> products = GetProducts();

            _logger.LogInformation("Updating product: " + updatedProduct.ProductName);

            int index = products.FindIndex(p => p.ProductId == id);

            if (index == -1)
            {
                return NotFound("Product not found");
            }

            updatedProduct.ProductId = id;
            products[index] = updatedProduct;

            _JSONService.WriteToFile(_filePath, products);

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            List<Product> products = GetProducts();

            int index = products.FindIndex(p => p.ProductId == id);

            if (index == -1)
            {
                return NotFound("Account not found");
            }
            _logger.LogInformation("Deleting account: " + products[index].ProductName);

            products.RemoveAt(index);

            _JSONService.WriteToFile(_filePath, products);

            return Ok("Success");
        }
    }
}
