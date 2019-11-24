using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie
{
    public class Startup
    {
        public Startup (IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            if (Environment.IsDevelopment ())
            {
                services.AddDbContext<RazorPagesMovieContext> (options =>
                    options.UseSqlite (Configuration.GetConnectionString ("MovieContext")));
            }
            else
            {
                services.AddDbContext<RazorPagesMovieContext> (options =>
                    options.UseSqlServer (Configuration.GetConnectionString ("MovieContext")));    
            }

            services.AddRazorPages ();
            
//            services.AddControllersWithViews ();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app)
        {
            if (Environment.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            }
            else
            {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

//            app.UseAuthorization ();

            app.UseEndpoints (endpoints =>
            {
                endpoints.MapRazorPages ();
//                endpoints.MapControllerRoute (
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}