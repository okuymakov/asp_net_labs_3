using asp_net_labs_3.Models;
using asp_net_labs_3.Repositories;
using asp_net_labs_3.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_net_labs_3
{
    public class UserManager
    {
        ClaimsIdentity Authenticate(Customer customer)
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

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
