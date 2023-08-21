using System.ComponentModel.DataAnnotations;

namespace EIP_A.Models
{
    public class UpdateSell
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String ProductName { get; set; }
        [Required]
        public string ClinetId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
