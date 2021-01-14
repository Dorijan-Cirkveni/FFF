using SkolaGitareAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories
{
    public abstract class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        protected GeneralRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(T item)
        {
            context.Set<T>().Add(item);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(object id)
        {
            var item = await context.FindAsync<T>(id);

            if(item == null)
            {
                return false;
            }

            context.Remove(item);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<T> Get(object id)
        {
            return await context.FindAsync<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public async Task<bool> Update(object id, T item)
        {
            context.Update(item);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
