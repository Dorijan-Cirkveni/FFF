using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface IGeneralRepository <T>
    {
        public IEnumerable<T> GetAll();

        public Task<T> Get(object id);

        public Task<bool> Update(object id, T item);

        public Task<bool> Create(T item);

        public Task<bool> Delete(object id);
    }
}
