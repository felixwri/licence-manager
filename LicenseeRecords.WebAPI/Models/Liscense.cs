using LicenseeRecords.WebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.WebAPI.Models
{
    public class Liscense
    {
        [Required]
        public int LicenceId { get; set; }
        [Required]
        public Status LicenceStatus { get; set; }
        [Required]
        public DateTime LicenceFromDate { get; set; }
        [Required]
        public DateTime? LicenceToDate { get; set; }
        [Required]
        public required Product Product { get; set; }
    }
}
