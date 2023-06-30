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
        public static IQueryable<T> ToDynamicWhereAndOrder<T>(this IQueryable<T> query, ListRequestDto p,
               string defaultField = "Name", string defaultDir = "ASC")
               where T : class
        {

            //if (!(p.Filter != null))
            //    query = query.Where(p.Filter);

            if (!(p.OrderDir.IsEmpty()) && !(p.OrderField.IsEmpty()))
                query = query.OrderBy(p.OrderField + " " + p.OrderDir);
            else
                query = query.OrderBy(defaultField + " " + defaultDir);
            return query;
        }

        public static List<T> ToDynamicWhereAndOrder<T>(this List<T> list, ListRequestDto p,
              string defaultField = "Name", string defaultDir = "ASC")
              where T : class
        {

            var query=list.AsQueryable();
            //if (!(p.Filter != null))
            //    query = query.Where(p.Filter);

            if (!(p.OrderDir.IsEmpty()) && !(p.OrderField.IsEmpty()))
                query = query.OrderBy(p.OrderField + " " + p.OrderDir);
            else
                query = query.OrderBy(defaultField + " " + defaultDir);
            return query.ToList();
        }
    }
}
