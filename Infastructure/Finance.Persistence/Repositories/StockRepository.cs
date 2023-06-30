using Finance.Application.Exceptions;
using Finance.Application.Repositories;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace Finance.Persistence.Repositories
{
    public class StockRepository : RedisCacheRepository<Stock>, IStockRepository
    {

        private readonly IAppUserRepository _userRepository;
        public StockRepository(AppData context, IAppUserRepository userRepository, IDistributedCache cache) : base(context,cache)
        {
            _userRepository = userRepository;
        }
        public override IQueryable<Stock> GetList(Expression<Func<Stock, bool>> filter = null)
        {
            var user = _userRepository.GetActiveUser().Result;
            var list = base.GetList(filter).Where(x => x.CompanyId == user.CompanyId.Value);
            return list;
        }
        public override async Task<bool> CreateAsync(Stock item)
        {
            var user = await _userRepository.GetActiveUser();
            item.CompanyId = user.CompanyId.Value;
            return await base.CreateAsync(item);
        }


        public async Task<bool> SetBalance(int id, decimal quantity)
        {
            var item = await GetItemAsync(id);
            if (item == null)
                throw new NotFoundException("");

            item.Balance += quantity;

            
            return await EditAsync(item);
        }
    }
}
