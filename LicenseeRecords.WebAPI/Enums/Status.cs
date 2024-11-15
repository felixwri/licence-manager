using System.Text.Json.Serialization;

namespace LicenseeRecords.WebAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Active,
        Inactive,
    }
}