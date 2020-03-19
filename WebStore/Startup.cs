using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.DAL.Context;
using WebStore.Data;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<WebStoreDBInitializer>();

            //services.AddMvc(); dot.net core 2.2(1,0)
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); //3.0 � ����

            //����������� �������
            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>(); // AddTransient - ������ ��� ����� ����������� ��������� �������
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>(); // AddScoped - ���� ��������� �� ������� ���������
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); // AddSingleton - ���� ������ �� ��� ����� ����� ����������
            services.AddSingleton<IProductData, InMemoryProductData>();
        }


        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEmployeesData employees)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        {
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //app.UseStaticFiles(new StaticFileOptions(new SharedOptions() { }) { });//��� ������ ����������� ������
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();
            Configuration["Testkey"] = "123";

            app.UseWelcomePage("/welcome"); //�������� ��������

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
