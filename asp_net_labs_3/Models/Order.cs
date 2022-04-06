using System;

namespace asp_net_labs_3.Models
{
    public class Order
	{
        public Order()
        {
        }

        public Order(Order order)
        {
			Id = order.Id;
			Customer = order.Customer;
			OrderStatus = order.OrderStatus;
			OrderDate = order.OrderDate;
			ShippedDate = order.ShippedDate;
			Product = order.Product;
			Address = order.Address;
			ProductPrice = order.ProductPrice;
			DeliveryPrice = order.DeliveryPrice;
			PaymentMethod = order.PaymentMethod;
			DeliveryMethod = order.DeliveryMethod;
		}
		public int Id { get; set; }
		public Customer Customer { get; set; }
		public string OrderStatus { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime? ShippedDate { get; set; }
		public Product Product { get; set; }
		public string Address { get; set; }
		public decimal ProductPrice { get; set; }
		public decimal DeliveryPrice { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
	}
}

