using System.Text.Json.Serialization;

namespace LicenseeRecords.Web.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Active,
        Inactive,
    }
}