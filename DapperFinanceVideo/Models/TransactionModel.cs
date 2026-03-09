namespace DapperFinanceVideo.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }

    }
}
