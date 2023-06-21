using Finance.Application.Repositories;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Persistence.Repositories
{
    public class PaymentTransactionRepository : Repository<PaymentTransaction>, IPaymentTransactionRepository
    {

        private readonly IAppUserRepository _userRepository;
        private readonly IStockRepository _stockRepository;
        public PaymentTransactionRepository(AppData context, IAppUserRepository userRepository, IStockRepository stockRepository) : base(context)
        {
            _userRepository = userRepository;
            _stockRepository = stockRepository;
        }

        public override IQueryable<PaymentTransaction> GetList(Expression<Func<PaymentTransaction, bool>> filter = null)
        {
            return base.GetList(filter).Include(x => x.Client).Include(x => x.Invoice);
        }
        public override async Task<bool> CreateAsync(PaymentTransaction item)
        {
            var result = await base.CreateAsync(item);
            _ = await _userRepository.SetClientPaymentBalance(item.ClientId, item.Price);
            return result;
        }
    }
}
