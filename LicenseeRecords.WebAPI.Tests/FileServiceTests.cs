using LicenseeRecords.WebAPI.Services;

namespace LicenseeRecords.WebAPI.Tests
{
    public class FileServiceTests
    {
        private readonly FileService _fileService;

        public FileServiceTests()
        {
            _fileService = new FileService();
        }

        [Fact]
        public void WriteToFile()
        {
            // Arrange
            var path = "Test_FileWrite.txt";
            var content = "Hello, World!";

            // Act
            _fileService.Write(path, content);

            // Assert
            var result = File.ReadAllText(path);
            Assert.Equal(content, result);

            // Cleanup
            File.Delete(path);
        }

        [Fact]
        public void ThrowsIfMissing()
        {
            var path = "Test_MissingFilePath.txt";
            // Assert
            Assert.Throws<FileNotFoundException>(() => _fileService.Read(path));
        }

        [Fact]
        public void ReadFile()
        {
            var path = "Test_FileRead.txt";
            var content = "Example content";
            _fileService.Write(path, content);

            // Act
            var result = _fileService.Read(path);

            // Assert
            Assert.Equal(result, content);

            File.Delete(path);
        }
    }
}