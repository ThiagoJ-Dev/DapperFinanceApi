using DapperFinanceVideo.DTO;
using DapperFinanceVideo.Models;
using DapperFinanceVideo.Repositories;

namespace DapperFinanceVideo.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public FinanceService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TransactionModel> AddTransactionAsync(TransactionModel t)
        {
            if (t.Amount <= 0)
            {
                throw new ArgumentException("A quantidade precisa ser superior a zero.");
            }
            if (t.Type != 'R' && t.Type != 'D')
            {
                throw new ArgumentException("O tipo precisa ser 'R' para receita ou 'D' para despesa.");
            }

            return await _transactionRepository.AddAsync(t);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsAsync(int? moth = null, int? year = null, char? type = null)
        {
            var all = (await _transactionRepository.GetAllAsync()).AsEnumerable();

            if (year.HasValue)
            {
                all = all.Where(t => t.Date.Year == year.Value);
            }
            if (moth.HasValue)
            {
                all = all.Where(t => t.Date.Month == moth.Value);
            }
            if (type.HasValue)
            {
                all = all.Where(t => t.Type == type.Value);
            }

            return all.OrderByDescending(t => t.Date);
        }

        public async Task<IEnumerable<CategoryTotalDto>> GetCategoryTotalsAsync(int? moth = null, int? year = null)
        {
            var all = (await _transactionRepository.GetAllAsync()).AsEnumerable();
            var categories = (await _categoryRepository.GetAllAsync()).ToList();

            if (year.HasValue)
            {
                all = all.Where(t => t.Date.Year == year.Value);
            }
            if (moth.HasValue)
            {
                all = all.Where(t => t.Date.Month == moth.Value);
            }

            var grouped = all.GroupBy(t => t.CategoryId)
                .Select(g =>
                {
                    var category = categories.FirstOrDefault(c => c.Id == g.Key);

                    return new CategoryTotalDto
                    {
                        CategoryId = g.Key,
                        CategoryName = category?.Name ?? "Categoria Desconecida",
                        Total = g.Sum(t => t.Amount)
                    };
                }).ToList();

            return grouped;

        }

        public async Task<DashboardDto> GetDashboardAsync(int moth, int year)
        {
            if (moth < 1 || moth > 12)
            {
                throw new ArgumentException("O mês precisa ser entre 1 e 12.");
            }
            if (year < 1 || year> 9999)
            {
                throw new ArgumentException("O ano precisa ser um número positivo.");
            }

            var all = (await _transactionRepository.GetAllAsync()).ToList();

            var atual = all.Where(t => t.Date.Month == moth && t.Date.Year == year);
            int prevMoth = moth == 1 ? 12 : moth - 1;
            int prevYear = moth == 1 ? year - 1 : year;


            IEnumerable<TransactionModel> anterior = Enumerable.Empty<TransactionModel>();

            if (prevYear >= 1)
            
                anterior = all.Where(t => t.Date.Month == prevMoth && t.Date.Year == prevYear);

                decimal receitas = atual.Where(t => t.Type == 'R').Sum(t => t.Amount);
                decimal despesas = atual.Where(t => t.Type == 'D').Sum(t => t.Amount);
                decimal saldo = receitas - despesas;

                decimal saldoAnterior = anterior.Any() ? 
                    anterior.Sum(t => t.Type == 'R' ? t.Amount : -t.Amount) : 0m;


                decimal diferenca = saldo - saldoAnterior;
                decimal percentual = saldoAnterior != 0 ? 
                    Math.Round(diferenca / Math.Abs(saldoAnterior) * 100, 2) : (diferenca == 0 ? 0 : 100);

                return new DashboardDto
                    {
                    Receitas = receitas,
                    Despesas = despesas,
                    Saldo = saldo,
                    SaldoAnterior = saldoAnterior,
                    Diferenca = diferenca,
                    PercentualCrescimento = percentual
                };
        }

        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task UpdateTransactionAsync(TransactionModel t)
        {
            if (t.Amount <= 0)
            {
                throw new ArgumentException("A quantidade precisa ser superior a zero.");
            }
            if (t.Type != 'R' && t.Type != 'D')
            {
                throw new ArgumentException("O tipo precisa ser 'R' para receita ou 'D' para despesa.");
            }

            await _transactionRepository.UpdateAsync(t);

        }
    }
}
