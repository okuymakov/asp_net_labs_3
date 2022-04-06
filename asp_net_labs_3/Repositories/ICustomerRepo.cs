using asp_net_labs_3.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_net_labs_3.Repositories
{
    public interface ICustomerRepo : ICRUDRepo<Customer>
    {
        public Task<Customer> GetByEmail(string email);

        //public IEnumerable<Customer> GetAll();
    }
}
