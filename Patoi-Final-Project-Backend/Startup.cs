using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patoi_Final_Project_Backend.DAL;
using System;
using Microsoft.AspNetCore.Identity;
using Patoi_Final_Project_Backend.Models;

namespace Patoi_Final_Project_Backend
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(100);
            });
            services.AddControllersWithViews();
            services.AddIdentity<AppUser, IdentityRole>(op =>
            {
                op.User.RequireUniqueEmail = true;
                op.Password.RequiredLength = 6;
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireDigit = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireUppercase = true;


                op.Lockout.AllowedForNewUsers = true;
                op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                op.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     "areas",
                     "{area:exists}/{controller=dashboard}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
