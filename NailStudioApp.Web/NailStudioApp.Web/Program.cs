using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using NailStudioApp.Data;
//using NailStudioApp.Data.Models;

namespace NailStudioApp.Web
{
    //using static Web.Infrastructure.Extensions.ExtensionMethods;
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddDbContext<NailStudioDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //string connectionString = builder.Configuration.GetConnectionString("SQLServer");
            //builder.Services.AddDbContext<NailStudioDbContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);
            //});

            //builder.Services.AddDefaultIdentity<ApplicationUser>()
            //  .AddEntityFrameworkStores<NailStudioDbContext>();
            //builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //})
            //  .AddRoles<IdentityRole<Guid>>()
            //   .AddEntityFrameworkStores<NailStudioDbContext>();


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
            
            //app.ApplyMigration();
            app.Run();
        }
    }
}
//Microsoft.Extensions.DependencyInjection.Abstractions
