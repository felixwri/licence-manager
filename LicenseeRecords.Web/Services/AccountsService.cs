using LicenseeRecords.Web.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace LicenseeRecords.Web.Services
{
    public interface IAccountsService
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task UpdateAccountAsync(int id, Account account);
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(int id);

    }

    public class AccountsService : IAccountsService
    {
        private readonly HttpClient _client;
        private readonly IJSONService _jsonService;
        private readonly string _UrlTarget = "accounts";
        public AccountsService(HttpClient client, IJSONService JSONService)
        {
            this._client = client;
            this._jsonService = JSONService;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + _UrlTarget);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var accounts = _jsonService.ReadJSON<IEnumerable<Account>>(responseContent) ?? [];

            return accounts;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/" + id;
            var response = await _client.GetAsync(URL);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var account = _jsonService.ReadJSON<Account>(responseContent);

            return account;
        }

        public async Task UpdateAccountAsync(int id, Account account)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/" + id;

            HttpContent httpContent = _jsonService.ConvertToJSON(account);

            var response = await _client.PutAsync(URL, httpContent);

            response.EnsureSuccessStatusCode();

            return;
        }

        public async Task AddAccountAsync(Account account)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/";

            HttpContent httpContent = _jsonService.ConvertToJSON(account);

            var response = await _client.PostAsync(URL, httpContent);

            response.EnsureSuccessStatusCode();

            return;
        }

        public async Task DeleteAccountAsync(int id)
        {
            string URL = _client.BaseAddress + _UrlTarget + "/" + id;

            var response = await _client.DeleteAsync(URL);

            response.EnsureSuccessStatusCode();

            return;
        }
    }
}
