using System.ComponentModel.DataAnnotations;

namespace EIP_A.Models
{
    public class CreateProduct
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int CostPrice { get; set; }
        [Required]
        public int SellingPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
    }
}
