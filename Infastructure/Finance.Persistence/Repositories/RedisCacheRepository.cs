using Finance.Application.Repositories;
using Finance.Domain.Entities.Common;
using Finance.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Finance.Application.Helpers;
using Finance.Persistence.Contexts;
using System.Security.Cryptography;
using Finance.Application.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finance.Persistence.Repositories
{
    public class RedisCacheRepository<T> :  Repository<T>, IRedisCacheRepository<T> where T : class 
    {
        private readonly IDistributedCache _cache;
        public RedisCacheRepository(AppData context,IDistributedCache cache):base(context)
        {
            _cache = cache;
        }



        public async override Task<bool> CreateAsync(T item)
        {
            var result = await base.CreateAsync(item);
            if (result)
            {
                var itemList = await GetListFromCacheAsync();
                itemList.Add(item);
                await UpdateCacheListAsync(itemList);
            }
            return result;
        }

        public override async Task<bool> EditAsync(T item)
        {
            var result = await base.EditAsync(item);
            if (result)
            {
                var itemList = await GetListFromCacheAsync();
                //var existingItem = itemList.Find(e => e.Id == item.Id);
                if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
                {
                    var id = $"{Digger.GetObjectValue(item, "Id")}";
                    int.TryParse(id, out int _id);
                    var existingItem = itemList.AsQueryable().Where($"Id=={_id}").FirstOrDefault();
                    if (existingItem != null)
                    {
                        itemList.Remove(existingItem);
                        itemList.Add(item);
                        await UpdateCacheListAsync(itemList);
                    }
                }
            }
            return result;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var result = await base.DeleteAsync(id);
            if (result)
            {
               
                var itemList = await GetListFromCacheAsync();
                if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
                {
                    int.TryParse(id.ToString(), out int _id);
                    var itemToRemove = itemList.AsQueryable().Where($"Id=={_id}").FirstOrDefault();
                    if (itemToRemove != null)
                    {
                        itemList.Remove(itemToRemove);
                        await UpdateCacheListAsync(itemList);
                    }
                }
            }
            return result;
        }

        public override async Task<bool> DeleteAsync(T item)
        {
            var result = await base.DeleteAsync(item);
            if (result)
            {
                var itemList = await GetListFromCacheAsync();
                if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
                {
                    var id = $"{Digger.GetObjectValue(item, "Id")}";
                    int.TryParse(id, out int _id);
                    var itemToRemove = itemList.AsQueryable().Where($"Id=={_id}").FirstOrDefault();
                    if (itemToRemove != null)
                    {
                        itemList.Remove(itemToRemove);
                        await UpdateCacheListAsync(itemList);
                    }
                }
            }
            return result;
        }

        public async Task<List<T>> GetListFromCacheAsync()
        {
           
            var cachedItemList = await _cache.GetStringAsync(typeof(T).Name);
     
            if (string.IsNullOrEmpty(cachedItemList))
            {
                var list = GetList().ToList();
               _ =await UpdateCacheListAsync(list);

                return list;
            }
            else
            {
              return  JsonConvert.DeserializeObject<List<T>>(cachedItemList);
            }
        }

        public async Task<bool> UpdateCacheListAsync(List<T> itemList)
        {
            await _cache.SetStringAsync(typeof(T).Name, JsonConvert.SerializeObject(itemList), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            });
            return true;
        }

        public async Task<bool> DeleteCacheListAsync()
        {
            await _cache.RemoveAsync(typeof(T).Name);
            return true;
        }


        public async Task<T> GetFromCacheAsync(int id)
        {
            var itemList = await GetListFromCacheAsync();
            var item = itemList.AsQueryable().Where($"Id=={id}").FirstOrDefault();
            
            if (item == null)
                throw new NotFoundException($"{typeof(T).Name} Id: {id} not found !");

            return item;
        }
    }
}
