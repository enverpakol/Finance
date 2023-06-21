using Finance.Domain.Entities;

namespace Finance.Application.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<bool> SetBalance(int id, decimal quantity);
    }
}
