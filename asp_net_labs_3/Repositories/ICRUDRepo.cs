using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_labs_3.Repositories
{
    public interface ICRUDRepo<T>
    {
        public Task<T> Create(T el);
        public Task Update(T el);
        public Task Delete(int id);
        public Task<T> Get(int id);
    }
}
