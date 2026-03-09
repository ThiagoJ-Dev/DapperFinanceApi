namespace DapperFinanceVideo.DTO
{
    public class CategoryTotalDto
    {
        public int CategoryId { get; set; }
        public decimal Total { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
