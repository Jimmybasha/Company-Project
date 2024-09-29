using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Data.Context;
using Company.G03.DAL.Interfaces;
using Company.G03.DAL.Model;
using Company.G03.PL.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

      

         


            builder.Services.AddDbContext<ApppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false; // No digits required
                options.Password.RequireLowercase = false; // No lowercase letters required
                options.Password.RequireUppercase = false; // No uppercase letters required
                options.Password.RequireNonAlphanumeric = false; // No special characters required
                options.Password.RequiredLength = 1; // Minimum length can be set to 1
                options.Password.RequiredUniqueChars = 0; // No unique character requirement
            }).AddEntityFrameworkStores<ApppDbContext>().AddDefaultTokenProviders();

			builder.Services.ConfigureApplicationCookie(config =>
			{
				config.LoginPath = "/Account/SignIn";
                config.AccessDeniedPath = "/Account/AccessDenied";

			});


			builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
