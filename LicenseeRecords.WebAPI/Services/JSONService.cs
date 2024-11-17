using System.Text.Json;

namespace LicenseeRecords.WebAPI.Services
{
    public class JSONService(IFileService fileService) : IJSONService
    {
        private readonly IFileService _fileService = fileService;

        public T ReadFile<T>(string path)
        {
            string content = _fileService.Read(path);
            T? result = JsonSerializer.Deserialize<T>(content);

            if (result == null)
            {
                throw new Exception("Deserialization failed.");
            }

            return result;
        }

        public void WriteToFile<T>(string path, T content)
        {
            string json = JsonSerializer.Serialize(content);
            _fileService.Write(path, json);
        }

        public T ReadString<T>(string content)
        {
            T? result = JsonSerializer.Deserialize<T>(content);

            if (result == null)
            {
                throw new Exception("Deserialization failed.");
            }

            return result;
        }

        public string WriteToString<T>(T content)
        {
            string jsonString = JsonSerializer.Serialize(content);
            return jsonString;
        }
    }
}
