using LicenseeRecords.WebAPI.Controllers;
using LicenseeRecords.WebAPI.Models;
using LicenseeRecords.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using System.Security.Principal;

namespace LicenseeRecords.WebAPI.Tests
{
    public class AccountsControllerTests
    {
        private readonly AccountsController _controller;
        private readonly IJSONService _JSONService;
        private readonly Mock<IJSONService> _mockJSONService;
        private readonly FakeLogger<AccountsController> _fakeLogger;
        private readonly string _filePath = Path.Combine(Environment.CurrentDirectory, "ExampleData", "TestAccounts.json");

        public AccountsControllerTests()
        {
            _JSONService = new JSONService(new FileService());
            _mockJSONService = new Mock<IJSONService>();
            _fakeLogger = new FakeLogger<AccountsController>();
            _controller = new AccountsController(_fakeLogger, _mockJSONService.Object);

            this.SetupMock();
        }

        private void SetupMock()
        {
            var accounts = _JSONService.ReadFile<List<Account>>(_filePath);
            string accountString = _JSONService.WriteToString(accounts[0]);

            _mockJSONService.Setup(s => s.ReadFile<List<Account>>(It.IsAny<string>())).Returns(accounts);
            _mockJSONService.Setup(s => s.ReadString<Account>(accountString)).Returns(accounts[0]);
            _mockJSONService.Setup(s => s.WriteToFile(It.IsAny<string>(), It.IsAny<List<Account>>()));
        }

        [Fact]
        public void GetAccounts()
        {
            var result = _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnAccounts = Assert.IsType<List<Account>>(okResult.Value);

            Assert.Single(returnAccounts);
        }

        [Fact]
        public void PostAccount()
        {
            var account = _JSONService.ReadFile<List<Account>>(_filePath)[0];
            string accountString = _JSONService.WriteToString(account);

            var result = _controller.Post(account);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal("Success", okResult.Value);

            _mockJSONService.Verify(s => s.WriteToFile(It.IsAny<string>(), It.IsAny<List<Account>>()), Times.Once);
        }

        [Fact]
        public void PutAccount()
        {
            var account = _JSONService.ReadFile<List<Account>>(_filePath)[0];
            var updatedAccount = new Account
            {
                AccountId = account.AccountId,
                AccountName = "Updated Name",
                AccountStatus = account.AccountStatus,
                ProductLicence = account.ProductLicence
            };

            var result = _controller.Put(account.AccountId, updatedAccount);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal("Success", okResult.Value);

            _mockJSONService.Verify(s => s.WriteToFile(It.IsAny<string>(), It.IsAny<List<Account>>()), Times.Once);
        }

        [Fact]
        public void DeleteAccount()
        {
            var account = _JSONService.ReadFile<List<Account>>(_filePath)[0];

            var result = _controller.Delete(account.AccountId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal("Success", okResult.Value);

            _mockJSONService.Verify(s => s.WriteToFile(It.IsAny<string>(), It.IsAny<List<Account>>()), Times.Once);
        }
    }
}