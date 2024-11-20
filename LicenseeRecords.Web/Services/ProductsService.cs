using LicenseeRecords.Web.Models;
using System.Text.Json;
using System.Text;

namespace LicenseeRecords.Web.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(int id, Product product);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);

    }
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private readonly string _UrlTarget = "products";
        public ProductsService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + _UrlTarget);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(responseContent, _jsonOptions) ?? [];

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/" + id;
            var response = await _client.GetAsync(URL);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<Product>(responseContent, _jsonOptions)!;

            return products;
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/" + id;

            var httpContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(URL, httpContent);

            response.EnsureSuccessStatusCode();

            return;
        }

        public async Task AddProductAsync(Product product)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/";

            var httpContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(URL, httpContent);

            response.EnsureSuccessStatusCode();

            return;
        }

        public async Task DeleteProductAsync(int id)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/" + id;

            var response = await _client.DeleteAsync(URL);

            response.EnsureSuccessStatusCode();

            return;
        }
    }
}
