using DapperFinanceVideo.Models;

namespace DapperFinanceVideo.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionModel>> GetAllAsync();
        Task<TransactionModel?> GetByIdAsync(int id);
        Task<TransactionModel> AddAsync(TransactionModel t);
        Task<int> UpdateAsync(TransactionModel t);
        Task<int> DeleteAsync(int id);
    }
}
