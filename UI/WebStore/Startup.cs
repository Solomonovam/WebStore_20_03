using AutoMapper;
using WebStore.Infrastructure.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Interfaces.Services;
using WebStore.Services.Products;
using WebStore.DAL.Context;
using WebStore.Services.Data;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InSQL;
using WebStore.Interfaces.Api;
using WebStore.Services.Products.InMemory;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Identity;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreDB>(opt =>
            //    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddTransient<WebStoreDBInitializer>();

                services.AddAutoMapper(opt =>
                {
                    opt.AddProfile<DTOMapping>();
                    opt.AddProfile<ViewModelsMapping>();
                }, typeof(Startup));



            services.AddIdentity<User, Role>()
                //.AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            #region WebAPI Identity clients stores

            services
               .AddTransient<IUserStore<User>, UsersClient>()
               .AddTransient<IUserPasswordStore<User>, UsersClient>()
               .AddTransient<IUserEmailStore<User>, UsersClient>()
               .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
               .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
               .AddTransient<IUserLockoutStore<User>, UsersClient>()
               .AddTransient<IUserClaimStore<User>, UsersClient>()
               .AddTransient<IUserLoginStore<User>, UsersClient>();
            services
               .AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion


            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklnmopqrstuvwxyzABCD....1234567890";
                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {

                opt.Cookie.Name = "WebStore";
                opt.Cookie.HttpOnly = true;
                //opt.Cookie.Expiration = TimeSpan.FromDays(10); //устарело
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            //services.AddMvc(); dot.net core 2.2(1,0)
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); //3.0 и выше

            //Регистрация сервиса
            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>(); // AddTransient - каждый раз будет создаваться экземпляр сервиса
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>(); // AddScoped - один экземпляр на обдасть видимости
            //services.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); // AddSingleton - один объект на все время жизни приложения
            services.AddSingleton<IEmployeesData, EmployeesClient>();

            //services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IProductData, ProductsClient>();

            services.AddScoped<ICartService, CookiesCartService>();

            //services.AddScoped<IOrderService, SqlOrderService>();
            services.AddScoped<IOrderService, OrdersClient>();

            services.AddScoped<IValuesService, ValuesClient>();
        }

        //Конвейер обработки
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEmployeesData employees)
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //app.UseStaticFiles(new StaticFileOptions(new SharedOptions() { }) { });//Для выдачи статических файлов
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            Configuration["Testkey"] = "123";

            app.UseWelcomePage("/welcome"); //тестовая страница

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["CustomGreetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
