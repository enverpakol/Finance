using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }

        IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);

        Task<T> GetItemAsync(int id);

        bool ValidateItem(T item);
        bool ValidateDeleteItem(T item);

        Task<bool> CreateAsync(T item);
        Task<bool> EditAsync(T item);

        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(T item);




    }
}
