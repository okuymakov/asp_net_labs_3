using asp_net_labs_3.Repositories;
using Dapper.FluentMap;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_net_labs_3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new CustomerMap());
                config.AddMap(new OrderMap());
                config.AddMap(new ProductMap());
            });

            services.AddTransient<ICustomerRepo,CustomerRepo>(provider => new CustomerRepo(connection));
            services.AddTransient<IOrderRepo,OrderRepo>(provider => new OrderRepo(connection));
            services.AddTransient<IProductRepo, ProductRepo>(provider => new ProductRepo(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => 
                {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   options.Events = new CookieAuthenticationEvents
                   {
                        OnValidatePrincipal = PrincipalValidator.ValidateAsync
                   };
                });

             services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/");                                  
            });
        }
    }

    public static class PrincipalValidator
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            if (context == null) throw new System.ArgumentNullException(nameof(context));

            int userId = int.Parse(context.Principal.FindFirstValue(ClaimTypes.NameIdentifier));


            var db = context.HttpContext.RequestServices.GetRequiredService<ICustomerRepo>();
            var customer = await db.Get(userId);
            if(customer != null)
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

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                context.ReplacePrincipal(new ClaimsPrincipal(claimsIdentity));
            }
        }
    }
}
