using asp_net_labs_3.Models;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_labs_3.Repositories
{
    public class OrderRepo : IOrderRepo
    {

        private string _сonnection;
        public OrderRepo(string connection)
        {
            _сonnection = connection;
        }

        public async Task<Order> Create(Order order)
        {
            using DbConnection db = new SqlConnection(_сonnection);        
            string sqlQuery = "insert into orders(order_date,customer_id,order_status,shipped_date,product_id,address,product_price," +
                "delivery_price,payment_method,delivery_method)" +
                "values(@orderdate,@customerid,@orderstatus,@shippeddate,@productid,@address,@productprice," +
                "@deliveryprice,@paymentmethod,@deliverymethod)";

            int id = await db.ExecuteAsync(sqlQuery,new {
                orderdate = order.OrderDate,
                customerid = order.Customer.Id,
                orderstatus = order.OrderStatus,
                shippeddate = order.ShippedDate,
                productid = order.Product.Id,
                address = order.Address,
                productprice = order.ProductPrice,
                deliveryprice = order.DeliveryPrice,
                paymentmethod = order.PaymentMethod,
                deliverymethod = order.DeliveryMethod
            });
            return new Order(order)
            {
                Id = id
            };
        }

        public async Task Delete(int id)
        {
            using DbConnection db = new SqlConnection(_сonnection);
            string sqlQuery = "delete from orders where order_id = @id";
            id = await db.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task<Order> Get(int id)
        {
            using DbConnection db = new SqlConnection(_сonnection);
            var sqlQuery = "select * from orders inner join customers on orders.customer_id = customers.customer_id " +
                "inner join products on orders.product_id = products.product_id";
            var order = await db.QueryAsync<Order,Customer,Product,Order>(sqlQuery,(order,customer,product) => {
                order.Customer = customer;
                order.Product = product;
                return order;
            }, new { id }, splitOn: "customer_id,product_id");

            return order.FirstOrDefault(c => c.Id == id); 
        }

        public async Task Update(Order order)
        {
            using DbConnection db = new SqlConnection(_сonnection);
            string sqlQuery = "update orders set order_date=@orderdate,order_status=@orderstatus,shipped_date=@shippeddate," +
                "address=@address,product_price=@productprice,delivery_price=@deliveryprice,payment_method=@paymentmethod," +
                "delivery_method=@deliverymethod where order_id = @id;";
            int id = await db.ExecuteAsync(sqlQuery, order);
        } 
        
    }
}
//customer_id,product_id,
                