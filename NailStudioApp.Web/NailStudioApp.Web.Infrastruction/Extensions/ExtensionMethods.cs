using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NailStudio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.Infrastruction.Extensions
{
    public static class ExtensionMethods
    {
        public static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            NailStudioAppDbContext dbCotext = serviceScope.ServiceProvider.GetRequiredService<NailStudioAppDbContext>()!;
            dbCotext.Database.Migrate();
            return app;

        }
    }
}
