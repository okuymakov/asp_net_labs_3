using asp_net_labs_3.Models;
using asp_net_labs_3.Repositories;
using asp_net_labs_3.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_net_labs_3.Controllers
{
    public class AccountController : Controller
    {
        private ICustomerRepo _repo;

        public AccountController(ICustomerRepo repo)
        {
            _repo = repo;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _repo.GetByEmail(model.Email);

                if (customer != null && PasswordHasher.VerifyPassword(customer.PasswordHash, model.Password))
                {
                    await Authenticate(customer);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Логин или пароль введены неверно");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Firstname = model.Firstname,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Dob = model.Dob,
                    Gender = model.Gender,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                    PasswordHash = PasswordHasher.HashPassword(model.Password)
                };
                if (await _repo.GetByEmail(customer.Email) == null) {
                    customer = await _repo.Create(customer);
                    await Authenticate(customer);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь уже зарегистрирован");
                }
                
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task Authenticate(Customer customer)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim("Firstname", customer.Firstname),
                new Claim("Surname", customer.Surname),
                new Claim("Patronymic", customer.Patronymic ?? ""),
                new Claim(ClaimTypes.DateOfBirth, customer.Dob?.ToString("d") ?? ""),
                new Claim(ClaimTypes.Gender, customer.Gender ?? ""),
                new Claim(ClaimTypes.StreetAddress, customer.Address ?? ""),               
                new Claim(ClaimTypes.MobilePhone, customer.Phone),             
            };

            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
