using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.Web.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(128)]
        public required string ProductName { get; set; }
    }
}
