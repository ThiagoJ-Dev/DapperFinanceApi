using Dapper;
using DapperFinanceVideo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperFinanceVideo.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);


        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Categories";
            return await connection.QueryAsync<CategoryModel>(sql);
        }

        public async Task<CategoryModel?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<CategoryModel>(sql, new { Id = id });
        }
    }
}
