using Finance.Domain.Entities;

namespace Finance.Application.Repositories
{
    public interface ICustomerRepository : IRedisCacheRepository<Customer>
    {
        Task<bool> SetPaymentBalance(int id, decimal quantity);
    }
}
