using LicenseeRecords.WebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.WebAPI.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        public required string AccountName { get; set; }

        [Required]
        public Status AccountStatus { get; set; }

        [Required]
        public List<Licence> ProductLicence { get; set; } = [];

    }
}
