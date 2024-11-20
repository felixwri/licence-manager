using System.Text.Json;

namespace LicenseeRecords.Web.Services
{
    public interface IJSONService
    {
        public T? ReadJSON<T>(string content);

        public StringContent ConvertToJSON<T>(T content);
    }
}
