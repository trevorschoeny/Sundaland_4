using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sundaland.Models;

namespace Sundaland
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });
            services.AddDbContext<AppIdentityDBContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]);
            });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            // Corresponds to wwwroot
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "CategoryPage",
                    "Category{category}/Page{pageNum}",
                    new { Controller = "Home", action = "Booklist" });

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Booklist" });

                endpoints.MapControllerRoute(
                    "Category",
                    "Category{category}",
                    new { Controller = "Home", action = "Booklist" });

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
