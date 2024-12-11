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
    using AutoMapper;
    using NailStudio.Data.Repository;
    using NailStudio.Data.Repository.Interfaces;
    using NailStudioApp.Service.MappingProfile;
    using NailStudioApp.Services.Data;
    using NailStudioApp.Services.Data.Interfaces;
    //using NailStudioApp.Services.Mapping.Mapping;
    //using NailStudioApp.Services.Mapping.Mapping;
    using NailStudioApp.Web.ViewModel.Service;
    using NailStudioApp.Webb.Models;
    using Services.Mapping;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email");
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:Username");
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password");
            

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
                  .AddRoles<IdentityRole<Guid>>()
                  .AddSignInManager<SignInManager<User>>()
                  .AddSignInManager<SignInManager<User>>();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Identity/Account/Login";
            });
            //builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            //builder.Services.AddScoped<IRepository<Service, Guid>, BaseRepository<Service, Guid>>();
            //builder.Services.AddScoped<IRepository<Appointment, Guid>, BaseRepository<Appointment, Guid>>();
            //builder.Services.AddScoped<IRepository<StaffMember, Guid>, BaseRepository<StaffMember, Guid>>();
            //builder.Services.AddScoped<IRepository<User, object>, BaseRepository<User, object>>();

            builder.Services.RegisterRepositories(typeof(User).Assembly);
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddAutoMapper(typeof(ServiceMappingProfile));
            builder.Services.AddAutoMapper(typeof(StaffMemberMappingProfile));
            builder.Services.AddAutoMapper(typeof(ReviewMappingProfile));
            builder.Services.AddAutoMapper(typeof(AppointmentServiceProfile));
            builder.Services.AddAutoMapper(typeof(ScheduleMappingProfile));
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IApoinmemetService, ApointmentService>();



            //builder.Services.AddScoped<IStaffMemberService, StaffMemberService>();
            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddProfile<StaffMemberMappingProfile>();
            //});
            //config.AssertConfigurationIsValid();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);


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
            //app.SeedAdministrator("admin@nailstudioapp.com", "Admin", "admin123");

            if (app.Environment.IsDevelopment())
            {
                app.SeedAdministrator(adminEmail, adminUsername, adminPassword);
                //app.SeedMovies(jsonPath);
            }

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.ApplyMigration();
            app.Run();
            
        }
    }
}
