namespace LicenseeRecords.WebAPI.Utils
{
    /// <summary>
    /// Data is moved to active on first start to prevent data being added to git and to allow for the default data to be restored.
    /// </summary>
    public class DataInitializer
    {
        public static void InitializeData()
        {
            if (File.Exists("Data/ActiveData/Accounts.json") && File.Exists("Data/ActiveData/Products.json"))
            {
                return;
            }

            Directory.CreateDirectory("Data/ActiveData");

            File.Copy("Data/Defaults/Accounts.json", "Data/ActiveData/Accounts.json", true);
            File.Copy("Data/Defaults/Products.json", "Data/ActiveData/Products.json", true);
        }
    }
}
