using System.Text;
using System.Text.Json;

namespace LicenseeRecords.Web.Services
{
    public class JSONService() : IJSONService
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public T? ReadJSON<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }

        public StringContent ConvertToJSON<T>(T content)
        {
            return new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }
    }
}
