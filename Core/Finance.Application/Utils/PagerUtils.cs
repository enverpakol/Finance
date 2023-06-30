using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.Utils
{
    public class PagerUtils<T, TDto> where T : class where TDto : class
    {
        public List<TDto> Items { get; set; }
        public PagerListInfoModel ItemsInfo { get; set; }


        public static PagerUtils<T, TDto> Set(IQueryable<T> data, IMapper mapper, int pageIndex, int pageSize = 15)

        {
            var model = new PagerUtils<T, TDto>
            {
                ItemsInfo = new PagerListInfoModel
                {
                    CurrentPageIndex = pageIndex,
                    CurrentPageSize = pageSize,
                    TotalItemsCount = data.Count()
                }
            };
            if (model.ItemsInfo.TotalItemsCount > 0)
                model.ItemsInfo.TotalPageCount = (int)Math.Ceiling(model.ItemsInfo.TotalItemsCount / (double)model.ItemsInfo.CurrentPageSize);
            if (model.ItemsInfo.TotalPageCount < model.ItemsInfo.CurrentPageIndex)
                model.ItemsInfo.CurrentPageIndex = model.ItemsInfo.TotalPageCount;

            int start = (model.ItemsInfo.CurrentPageIndex - 1) * model.ItemsInfo.CurrentPageSize;
            if (model.ItemsInfo.TotalPageCount > 0)
            {
                var query2 = data.Skip(start).Take(model.ItemsInfo.CurrentPageSize);
                model.Items = query2.Select(d => mapper.Map<T, TDto>(d)).ToList();
            }

            return model;
        }
        public async static Task<PagerUtils<T, TDto>> SetAsync(IQueryable<T> data, IMapper mapper, int pageIndex, int pageSize = 15)

        {
            var model = new PagerUtils<T, TDto>
            {
                ItemsInfo = new PagerListInfoModel
                {
                    CurrentPageIndex = pageIndex,
                    CurrentPageSize = pageSize,
                    TotalItemsCount = await data.CountAsync()
                }
            };
            if (model.ItemsInfo.TotalItemsCount > 0)
                model.ItemsInfo.TotalPageCount = (int)Math.Ceiling(model.ItemsInfo.TotalItemsCount / (double)model.ItemsInfo.CurrentPageSize);
            if (model.ItemsInfo.TotalPageCount < model.ItemsInfo.CurrentPageIndex)
                model.ItemsInfo.CurrentPageIndex = model.ItemsInfo.TotalPageCount;

            int start = (model.ItemsInfo.CurrentPageIndex - 1) * model.ItemsInfo.CurrentPageSize;
            if (model.ItemsInfo.TotalPageCount > 0)
            {
                var query2 = data.Skip(start).Take(model.ItemsInfo.CurrentPageSize);
                model.Items = await query2.Select(d => mapper.Map<T, TDto>(d)).ToListAsync();
            }

            return model;
        }
        public  static PagerUtils<T, TDto> SetAsync(List<T> data, IMapper mapper, int pageIndex, int pageSize = 15)

        {
            var model = new PagerUtils<T, TDto>
            {
                ItemsInfo = new PagerListInfoModel
                {
                    CurrentPageIndex = pageIndex,
                    CurrentPageSize = pageSize,
                    TotalItemsCount =  data.Count()
                }
            };
            if (model.ItemsInfo.TotalItemsCount > 0)
                model.ItemsInfo.TotalPageCount = (int)Math.Ceiling(model.ItemsInfo.TotalItemsCount / (double)model.ItemsInfo.CurrentPageSize);
            if (model.ItemsInfo.TotalPageCount < model.ItemsInfo.CurrentPageIndex)
                model.ItemsInfo.CurrentPageIndex = model.ItemsInfo.TotalPageCount;

            int start = (model.ItemsInfo.CurrentPageIndex - 1) * model.ItemsInfo.CurrentPageSize;
            if (model.ItemsInfo.TotalPageCount > 0)
            {
                var query2 = data.Skip(start).Take(model.ItemsInfo.CurrentPageSize);
                model.Items =  query2.Select(d => mapper.Map<T, TDto>(d)).ToList();
            }

            return model;
        }
    }

    public class PagerListInfoModel
    {
        public int TotalItemsCount { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int CurrentPageSize { get; set; }
    }
}
