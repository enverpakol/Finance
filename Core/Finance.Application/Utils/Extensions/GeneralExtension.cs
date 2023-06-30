using Finance.Domain.Entities.Identity;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata.Ecma335;

namespace Finance.Application.Utils.Extensions
{
    public static class GeneralExtension
    {
        public static List<int> GetOnlyIdList(this Task<IList<AppUser>> service)
        {
            var data = service.Result;
            return data.Select(x => x.Id).ToList();
        }
        public static IQueryable<T> ToDynamicOrder<T>(this IQueryable<T> query, string field = null, string direction = null)
               where T : class
        {
            if (!(field.IsEmpty()) && !(direction.IsEmpty()))
                query = query.OrderBy(field + " " + direction);
            return query;
        }

        public static List<T> ToDynamicOrder<T>(this List<T> list,string field = null, string direction = null)
              where T : class
        {
            var query=list.AsQueryable();

            if (!(field.IsEmpty()) && !(direction.IsEmpty()))
                query = query.OrderBy(field+ " " + direction);

            return query.ToList();
        }
    }
}
