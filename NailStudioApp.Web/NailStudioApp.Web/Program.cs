using Microsoft.EntityFrameworkCore;
using NailStudioApp.Data;

namespace NailStudioApp.Web
{
    using static Web.Infrastructure.Extensions.ExtensionMethods;
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("SQLServer");
            builder.Services.AddDbContext<NailStudioDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });   
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.ApplyMigration();
            app.Run();
        }
    }
}
