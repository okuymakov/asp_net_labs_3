using asp_net_labs_3.Models;
using asp_net_labs_3.Repositories;
using asp_net_labs_3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace asp_net_labs_3.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepo _repo;

        public OrderController(IOrderRepo repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            return Json(5);
        }
        public async Task<JsonResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await HttpContext.RequestServices.
                    GetRequiredService<ICustomerRepo>().Get(model.CustomerId);
                var product = await HttpContext.RequestServices.
                    GetRequiredService<IProductRepo>().Get(model.ProductId);
                var order = new Order
                {
                    OrderDate = System.DateTime.Now,
                    Customer = customer,
                    OrderStatus = "Подтвержден",
                    ShippedDate = null,
                    Product = product,
                    Address = model.Address,
                    ProductPrice = product.Price,
                    DeliveryPrice = 0,
                    DeliveryMethod = model.DeliveryMethod,
                    PaymentMethod = model.PaymentMethod,
                };

                await _repo.Create(order);
                return Json(true);
            }
            return Json(false);
        }
    }
}
