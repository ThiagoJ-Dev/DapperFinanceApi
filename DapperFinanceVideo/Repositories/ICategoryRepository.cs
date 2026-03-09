using DapperFinanceVideo.Models;

namespace DapperFinanceVideo.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel> GetByIdAsync(int id);
    }
}
