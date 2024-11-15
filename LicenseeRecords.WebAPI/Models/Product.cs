using System.ComponentModel.DataAnnotations;

namespace LicenseeRecords.WebAPI.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}
