using LicenseeRecords.WebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.WebAPI.Models
{
    public class Liscense
    {
        [Required]
        public int LiscenseId { get; set; }
        [Required]
        public Status LiscenseStatus { get; set; }
        [Required]
        public DateTime LiscenseFromDate { get; set; }
        [Required]
        public DateTime? LiscenseToDate { get; set; }
        [Required]
        public required Product Product { get; set; }
    }
}
