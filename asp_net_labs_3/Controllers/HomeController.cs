using asp_net_labs_3.Models;
using asp_net_labs_3.Repositories;
using asp_net_labs_3.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace asp_net_labs_3.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepo _repo;

        public HomeController(IProductRepo repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult> Index()
        {
            var products = await _repo.GetAll();
            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
