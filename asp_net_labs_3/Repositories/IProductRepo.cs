using asp_net_labs_3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_net_labs_3.Repositories
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(int id);
    }
}
