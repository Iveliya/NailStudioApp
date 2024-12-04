using Microsoft.EntityFrameworkCore;
using NailStudioApp.Data;
using NailStudio.Data.Models;
using Microsoft.AspNetCore.Identity;
using NailStudioApp.Web.Infrastruction.Extensions;
using NailStudio.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Configuration;

namespace NailStudioApp.Webb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<NailStudio.Data.NailDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-AEUQ5AJ\\SQLEXPRESS;Database=NailStudio;Trusted_Connection=True;TrustServerCertificate=True")
            );

            builder.Services
                  .AddIdentity<User, IdentityRole<Guid>>(options =>
                  {
                      options.Password.RequireDigit = false;
                      options.Password.RequireLowercase = false;
                      options.Password.RequireNonAlphanumeric = false;
                      options.Password.RequireUppercase = false;
                      options.Password.RequiredLength = 6;
                      options.Password.RequiredUniqueChars = 1;
                      options.SignIn.RequireConfirmedAccount = false;

                      // Lockout settings
                      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                      options.Lockout.MaxFailedAccessAttempts = 5;
                      options.Lockout.AllowedForNewUsers = true;

                      // User settings
                      options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                      options.User.RequireUniqueEmail = true;

                      // SignIn settings
                      options.SignIn.RequireConfirmedEmail = false;
                      options.SignIn.RequireConfirmedPhoneNumber = false;
                  })
                  .AddEntityFrameworkStores<NailDbContext>()
                  .AddDefaultTokenProviders();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
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
            app.MapRazorPages();
            //app.ApplyMigration();
            app.Run();
            
        }
    }
}
