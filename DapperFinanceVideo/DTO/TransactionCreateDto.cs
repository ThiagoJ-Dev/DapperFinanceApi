using System.ComponentModel.DataAnnotations;

namespace DapperFinanceVideo.DTO
{
    public class TransactionCreateDto
    {
        [Required]
        public char Type { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? Description { get; set; }
    }
}
