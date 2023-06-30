using Finance.Domain.Entities;

namespace Finance.Application.Repositories
{
    public interface IStockRepository : IRedisCacheRepository<Stock>
    {
        Task<bool> SetBalance(int id, decimal quantity);
    }
}
