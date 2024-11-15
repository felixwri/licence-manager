namespace LicenseeRecords.WebAPI.Services
{
    public interface IJSONService
    {
        public T Read<T>(string path);

        public void Write<T>(string path, T content);
    }
}
