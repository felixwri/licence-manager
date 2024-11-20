using LicenseeRecords.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.Web.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        [MaxLength(128)]
        public required string AccountName { get; set; }

        [Required]
        public Status AccountStatus { get; set; }

        [Required]
        public List<Licence> ProductLicence { get; set; } = [];

    }
}
