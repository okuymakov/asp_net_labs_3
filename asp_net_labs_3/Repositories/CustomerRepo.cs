using asp_net_labs_3.Models;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_labs_3.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private string _connection;
        public CustomerRepo(string connection)
        {
            _connection = connection;
        }

        public async Task<Customer> Create(Customer customer)
        {
            using DbConnection db = new SqlConnection(_connection);
            string sqlQuery = "insert into customers(firstname,surname,patronymic,gender,dob,address,phone,email,password_hash)" +
                "values(@firstname,@surname,@patronymic,@gender,@dob,@address,@phone,@email,@passwordhash)";
            int id = await db.ExecuteAsync(sqlQuery, customer);

            return new Customer(customer)
            {
                Id = id
            };
        }

        public async Task Delete(int id)
        {
            using DbConnection db = new SqlConnection(_connection);
            string sqlQuery = "delete from Customers where customer_id = @id";
            id = await db.ExecuteAsync(sqlQuery, new { id });   
        }
        public async Task<Customer> Get(int id)
        {
            using DbConnection db = new SqlConnection(_connection);
            var sqlQuery = "select * from Customers left join orders on orders.customer_id = customers.customer_id " +
                "left join products on orders.product_id = products.product_id";
            var customers = await db.QueryAsync<Customer, Order, Product, Customer>(sqlQuery, (customer, order, product) =>
            {
                if (order != null)
                { 
                    order.Product = product;
                    customer.Orders.Add(order);
                }
                return customer;
            }, splitOn: "order_id,product_id");
            
            var gCustomers = customers.GroupBy(c => c.Id).Select(g =>
            {
                var groupedCustomer = g.FirstOrDefault();
                if (groupedCustomer.Orders.Count() != 0)
                {
                    groupedCustomer.Orders = g.Select(c => c.Orders.SingleOrDefault()).ToList();
                }
                
                return groupedCustomer;
            });

            return gCustomers.FirstOrDefault(c => c.Id == id);
        }
        public async Task Update(Customer customer)
        {
            using DbConnection db = new SqlConnection(_connection);           
            string sqlQuery = "update customers set firstname=@firstname,surname=@surname,patronymic=@patronymic," +
                "gender=@gender,dob=@dob,address=@address,phone=@phone,email=@email where customer_id = @id;";
            int id = await db.ExecuteAsync(sqlQuery, customer);   
        }
        public async Task<Customer> GetByEmail(string email)
        {
            using DbConnection db = new SqlConnection(_connection);         
            var customer = await db.QueryFirstOrDefaultAsync<Customer>("select * from Customers where email = @email", new { email });
            return customer;
        }
    }
}
