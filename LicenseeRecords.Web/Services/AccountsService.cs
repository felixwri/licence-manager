using LicenseeRecords.WebAPI.Models;
using System.Net.Http;
using System.Text.Json;

namespace LicenseeRecords.Web.Services
{
    public class AccountsService
    {
        private readonly HttpClient _client;
        public AccountsService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + "accounts");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var accounts = JsonSerializer.Deserialize<IEnumerable<Account>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return accounts;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            string URL = _client.BaseAddress + "accounts/" + id;
            Console.WriteLine(URL);
            var response = await _client.GetAsync(URL);

            

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseContent);

            var account = JsonSerializer.Deserialize<Account>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return account;
        }
    }
}
