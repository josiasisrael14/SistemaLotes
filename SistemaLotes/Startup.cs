using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaLotes.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SistemaLotes
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {



            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<SistemaLotesDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("conexion")));

            //services.AddScoped<db>();
            services.AddTransient<db>();
            //services.AddSingleton<db>();
            services.AddSession();
            services.AddRazorPages().AddRazorRuntimeCompilation();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     //pattern: "{controller=registrogeneral}/{action=Index}/{id?}");
                     pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }


    }

}
    
