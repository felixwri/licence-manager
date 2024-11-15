namespace LicenseeRecords.WebAPI.Services
{
    public class FileService : IFileService
    {
        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.", path);  
            

            string content = File.ReadAllText(path);
            return content;
        }

        public void Write(string path, string content)
        {
            File.WriteAllText(path, content);
        }

    }
}
