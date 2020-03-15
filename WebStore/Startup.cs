using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); dot.net core 2.2(1,0)
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); //3.0 и выше

            //–егистраци€ сервиса
            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>(); // AddTransient - каждый раз будет создаватьс€ экземпл€р сервиса
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>(); // AddScoped - один экземпл€р на обдасть видимости
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); // AddSingleton - один объект на все врем€ жизни приложени€
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //app.UseStaticFiles(new StaticFileOptions(new SharedOptions() { }) { });//ƒл€ выдачи статических файлов
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();
            Configuration["Testkey"] = "123";

            app.UseWelcomePage("/welcome"); //тестова€ страница

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["CustomGreetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
