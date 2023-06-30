using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Repositories
{
    public interface IRedisCacheRepository<T> : IRepository<T> where T : class
    {
        Task<List<T>> GetListFromCacheAsync();
        Task<T> GetFromCacheAsync(int id);
        Task<bool> UpdateCacheListAsync(List<T> itemList);
        Task<bool> DeleteCacheListAsync();
    }
}
