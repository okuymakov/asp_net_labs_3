using asp_net_labs_3.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_labs_3.Repositories
{
    public class ProductRepo : IProductRepo
    {

        string Connection;
        public ProductRepo(string connection)
        {
            Connection = connection;
        }

        public async Task<Product> Get(int id)
        {
            using DbConnection db = new SqlConnection(Connection);
            var product = await db.QueryFirstOrDefaultAsync<Product>("select * from products where product_id = @id",new { id });
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using DbConnection db = new SqlConnection(Connection);
            var products = await db.QueryAsync<Product>("select * from products");
            return products;
        }
    }
}
