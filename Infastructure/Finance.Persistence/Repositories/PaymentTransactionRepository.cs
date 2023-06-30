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

        private readonly IStockRepository _stockRepository;
        private readonly ICustomerRepository customerRepository;
        public PaymentTransactionRepository(AppData context, IAppUserRepository userRepository, IStockRepository stockRepository, ICustomerRepository customerRepository) : base(context)
        {
            _stockRepository = stockRepository;
            this.customerRepository = customerRepository;
        }

        public override IQueryable<PaymentTransaction> GetList(Expression<Func<PaymentTransaction, bool>> filter = null)
        {
            return base.GetList(filter).Include(x => x.Customer).Include(x => x.Invoice);
        }
        public override async Task<bool> CreateAsync(PaymentTransaction item)
        {
            var result = await base.CreateAsync(item);
            _ = await customerRepository.SetPaymentBalance(item.CustomerId, item.Price);
            return result;
        }
    }
}
