using System.ComponentModel.DataAnnotations;

namespace EIP_A.Models
{
    public class DeletedSell
    {
        public int Id { get; set; }

        public String ProductName { get; set; }

        public string? ClinetId { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
