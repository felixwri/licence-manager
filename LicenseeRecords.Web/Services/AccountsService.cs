using LicenseeRecords.WebAPI.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace LicenseeRecords.Web.Services
{
    public class AccountsService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public AccountsService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + "accounts");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var accounts = JsonSerializer.Deserialize<IEnumerable<Account>>(responseContent, _jsonOptions);

            return accounts;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            string URL = _client.BaseAddress + "accounts/" + id;
            var response = await _client.GetAsync(URL);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var account = JsonSerializer.Deserialize<Account>(responseContent, _jsonOptions);

            return account;
        }

        public async Task UpdateAccountAsync(int id, Account account)
        {
            string URL = _client.BaseAddress + "accounts/" + id;

            var httpContent = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(URL, httpContent);

            var status = response.EnsureSuccessStatusCode();

            return;
        }
    }
}
