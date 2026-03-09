using DapperFinanceVideo.DTO;
using DapperFinanceVideo.Models;

namespace DapperFinanceVideo.Services
{
    public interface IFinanceService
    {
        Task<IEnumerable<TransactionModel>> GetTransactionsAsync(int? moth = null, int? year = null, char? type = null);
        Task<TransactionModel?> GetTransactionByIdAsync(int id);
        Task<TransactionModel> AddTransactionAsync (TransactionModel t);
        Task UpdateTransactionAsync (TransactionModel t);
        Task DeleteTransactionAsync (int id);

        Task<DashboardDto> GetDashboardAsync (int moth, int year);
        Task<IEnumerable<CategoryTotalDto>> GetCategoryTotalsAsync(int? moth = null, int? year = null);
    }
}
