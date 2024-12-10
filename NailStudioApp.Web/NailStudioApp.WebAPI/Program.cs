
using Microsoft.EntityFrameworkCore;
using NailStudio.Data.Models;
using NailStudioApp.Services.Mapping.Mapping;
using NailStudioApp.Web.Infrastruction.Extensions;

namespace NailStudioApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<NailStudio.Data.NailDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-AEUQ5AJ\\SQLEXPRESS;Database=NailStudio;Trusted_Connection=True;TrustServerCertificate=True")
            );
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.RegisterRepositories(typeof(User).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
