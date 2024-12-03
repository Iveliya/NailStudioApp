using Microsoft.EntityFrameworkCore;
using NailStudioApp.Data;
using NailStudio.Data.Models;
using Microsoft.AspNetCore.Identity;
using NailStudioApp.Web.Infrastruction.Extensions;
using NailStudio.Data;

namespace NailStudioApp.Webb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("NailStudioAppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'NailStudioAppDbContextConnection' not found.");


            builder.Services.AddDbContext<NailStudioAppDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-AEUQ5AJ\\SQLEXPRESS;Database=NailStudioDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"));


            builder.Services.AddDefaultIdentity<ApplicationUser>()
              .AddEntityFrameworkStores<NailStudioAppDbContext>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
            app.ApplyMigration();
            app.Run();
        }
    }
}
