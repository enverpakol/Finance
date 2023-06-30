using Finance.Application.Exceptions;
using Finance.Application.Repositories;
using Finance.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Finance.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppData Context;

        public Repository(AppData context)
        {
            Context = context;
        }


        public DbSet<T> Table => Context.Set<T>();

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Table.AsNoTracking();
            if (filter != null)
                query = query.Where(filter);
            return query;
        }
        public virtual async Task<T> GetItemAsync(int id)
        {
            var data = await Table.FindAsync(id);
            if (data == null)
                throw new NotFoundException($"{typeof(T).Name} Id: {id} not found !");
            return data;
        }
           

        public virtual bool ValidateItem(T item)
        {
            return true;
        }
        public virtual bool ValidateDeleteItem(T item)
        {
            return true;
        }

        public virtual async Task<bool> CreateAsync(T item)
        {
            if (!ValidateItem(item))
                return false;
            Table.Add(item);
            await Context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> EditAsync(T item)
        {
            if (!ValidateItem(item))
                return false;
            Context.Entry(item).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            T entityToDelete = await Table.FindAsync(id);
            return await DeleteAsync(entityToDelete);
        }
        public virtual async Task<bool> DeleteAsync(T item)
        {
            if (!ValidateDeleteItem(item))
                return false;
            if (Context.Entry(item).State == EntityState.Detached)
                Table.Attach(item);
            Table.Remove(item);
            await Context.SaveChangesAsync();
            return true;
        }


    }
}
