using asp_net_labs_3.Models;
using asp_net_labs_3.Repositories;
using asp_net_labs_3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_net_labs_3.Controllers
{

   [Authorize]
    public class ProfileController : Controller
    {
        private ICustomerRepo _repo;

        public ProfileController(ICustomerRepo repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult> OrdersAsync()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer = await _repo.Get(id);

            return View(customer.Orders);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CustomerEditViewModel model)
        {
            if(ModelState.IsValid)
            {  
                var customer = new Customer
                {
                    Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    Firstname = model.Firstname,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Dob = model.Dob,
                    Gender = model.Gender,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                };
                await _repo.Update(customer);
            }
            return View(model);
        }

        
        public async Task<ActionResult> Delete()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _repo.Delete(id);
            return RedirectToAction("Login", "Account");
        }
    }
}
