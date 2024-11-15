namespace LicenseeRecords.WebAPI.Services
{
    public interface IFileService
    {
        public string Read(string path);

        public void Write(string path, string content);

    }
}
