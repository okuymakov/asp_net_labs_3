using asp_net_labs_3.Models;
using Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace asp_net_labs_3.Repositories
{
    class CustomerMap : EntityMap<Customer>
    {
        public CustomerMap()
        {
            Map(c => c.Id)
                .ToColumn("customer_id");
            Map(c => c.PasswordHash).ToColumn("password_hash");

        }
    }
    class ProductMap : EntityMap<Product>
    {
        public ProductMap()
        {
            Map(p => p.Id)
                .ToColumn("product_id");
           

        }
    }
    class OrderMap : EntityMap<Order>
    {
        public OrderMap()
        {
            Map(o => o.Id).ToColumn("order_id");
            Map(o => o.OrderDate).ToColumn("order_data");
            Map(o => o.OrderStatus).ToColumn("order_status");
            Map(o => o.ShippedDate).ToColumn("shipped_data");
            Map(o => o.ProductPrice).ToColumn("product_price");
            Map(o => o.DeliveryPrice).ToColumn("delivery_price");
            Map(o => o.PaymentMethod).ToColumn("payment_method");
            Map(o => o.DeliveryMethod).ToColumn("delivery_method");
        }
    }
}

