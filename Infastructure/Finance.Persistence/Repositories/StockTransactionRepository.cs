using Finance.Application.Repositories;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Persistence.Repositories
{
    public class StockTransactionRepository : Repository<StockTransaction>, IStockTransactionRepository
    {

        private readonly IAppUserRepository _userRepository;
        private readonly IStockRepository _stockRepository;
        public StockTransactionRepository(AppData context, IAppUserRepository userRepository, IStockRepository stockRepository) : base(context)
        {
            _userRepository = userRepository;
            _stockRepository = stockRepository;
        }

        public override IQueryable<StockTransaction> GetList(Expression<Func<StockTransaction, bool>> filter = null)
        {
            return base.GetList(filter).Include(x=>x.Stock).Include(x=>x.Invoice);
        }
        public override async Task<bool> CreateAsync(StockTransaction item)
        {
            var result = await base.CreateAsync(item);
            _ = await _stockRepository.SetBalance(item.StockId, item.Quantity);
            return result;
        }
    }
}
