namespace LicenseeRecords.WebAPI.Services
{
    public interface IJSONService
    {
        public T ReadFile<T>(string path);

        public void WriteToFile<T>(string path, T content);

        public T ReadString<T>(string content);

        public string WriteToString<T>(T content);
    }
}
