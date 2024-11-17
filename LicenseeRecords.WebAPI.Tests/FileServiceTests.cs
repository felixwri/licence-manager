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
            var path = "Test_FileWrite.txt";
            var content = "Hello, World!";

            _fileService.Write(path, content);

            var result = File.ReadAllText(path);
            Assert.Equal(content, result);

            File.Delete(path);
        }

        [Fact]
        public void ThrowsIfMissing()
        {
            var path = "Test_MissingFilePath.txt";
            Assert.Throws<FileNotFoundException>(() => _fileService.Read(path));
        }

        [Fact]
        public void ReadFile()
        {
            var path = "Test_FileRead.txt";
            var content = "Example content";
            _fileService.Write(path, content);

            var result = _fileService.Read(path);

            Assert.Equal(result, content);

            File.Delete(path);
        }
    }
}