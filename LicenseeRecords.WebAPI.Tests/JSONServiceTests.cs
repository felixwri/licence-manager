using LicenseeRecords.WebAPI.Models;
using LicenseeRecords.WebAPI.Services;

namespace LicenseeRecords.WebAPI.Tests
{
    
    public class MockObject
    {
        public string ProductName { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }

    public class JSONServiceTests
    {
        private readonly JSONService _jsonService;

        public JSONServiceTests()
        {
            _jsonService = new JSONService(new FileService());
        }

        [Fact]
        public void WriteJSONToFile()
        {
            var path = "Test_JSONWrite.json";
            var content = new { ProductName = "Betting", ProductId = 30 };

            _jsonService.WriteToFile(path, content);

            var result = File.ReadAllText(path);
            Assert.Equal("{\"ProductName\":\"Betting\",\"ProductId\":30}", result);

            File.Delete(path);
        }

        [Fact]
        public void ReadJSONFromFile()
        {
            var path = "Test_JSONRead.json";
            var content = new MockObject { ProductName = "Betting", ProductId = 25 };
            _jsonService.WriteToFile(path, content);

            var result = _jsonService.ReadFile<MockObject>(path);

            Assert.Equal(result.ProductName, content.ProductName);
            Assert.Equal(result.ProductId, content.ProductId);

            File.Delete(path);
        }

        [Fact]
        public void DeserializeAllAcounts()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "ExampleData", "TestAccounts.json");

            List<Account> accounts = _jsonService.ReadFile<List<Account>>(filePath);

            Assert.NotEmpty(accounts);

            Account account = accounts[0];

            Assert.Equal(1, account.AccountId);
            Assert.Equal("Online Bingo Limited", account.AccountName);
            Assert.Equal(Enums.Status.Active, account.AccountStatus);
            Assert.Single(account.ProductLicence);
            Assert.Equal("Bingo", account.ProductLicence[0].Product.ProductName);
        }
    }
}