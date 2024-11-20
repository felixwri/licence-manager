using LicenseeRecords.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.Web.Models
{
    public class Licence
    {
        [Required]
        public int LicenceId { get; set; }
        [Required]
        public Status LicenceStatus { get; set; }
        [Required]
        public DateTime LicenceFromDate { get; set; }

        public DateTime? LicenceToDate { get; set; } = null;
        [Required]
        public required Product Product { get; set; }
    }
}
