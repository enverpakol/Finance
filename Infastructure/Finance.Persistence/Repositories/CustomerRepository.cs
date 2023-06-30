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
    public class CustomerRepository : RedisCacheRepository<Customer>, ICustomerRepository
    {

        private readonly IAppUserRepository _userRepository;
        public CustomerRepository(AppData context, IAppUserRepository userRepository, IDistributedCache cache) : base(context,cache)
        {
            _userRepository = userRepository;
        }
        public override IQueryable<Customer> GetList(Expression<Func<Customer, bool>> filter = null)
        {
            var user = _userRepository.GetActiveUser().Result;
            var list = base.GetList(filter).Where(x => x.CompanyId == user.CompanyId.Value);
            return list;
        }
        public override async Task<bool> CreateAsync(Customer item)
        {
            var user = await _userRepository.GetActiveUser();
            item.CompanyId = user.CompanyId.Value;
            return await base.CreateAsync(item);
        }


        public async Task<bool> SetPaymentBalance(int id, decimal quantity)
        {
            var item = await GetItemAsync(id);
            if (item == null)
                throw new NotFoundException("");

            item.BalanceDebt += quantity;

            return await EditAsync(item);
        }
    }
}
