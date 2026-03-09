using Dapper;
using DapperFinanceVideo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperFinanceVideo.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<TransactionModel> AddAsync(TransactionModel t)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO Transactions (Type, CategoryId, Description, Amount, Date)
                            OUTPUT INSERTED.*
                            VALUES (@Type, @CategoryId, @Description, @Amount, @Date)";

            var response = await connection.QuerySingleAsync<TransactionModel>(sql, t);

            return response;
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM Transactions WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<TransactionModel>> GetAllAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Transactions";
            return await connection.QueryAsync<TransactionModel>(sql);
        }

        public async Task<TransactionModel?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Transactions WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<TransactionModel>(sql, new { Id = id }); 
        }

        public async Task<int> UpdateAsync(TransactionModel t)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE Transactions 
                        SET Type = @Type, 
                                    CategoryId = @CategoryId, 
                                    Description = @Description, 
                                    Amount = @Amount, 
                                    Date = @Date
                        WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, t);
        }
    }
}
