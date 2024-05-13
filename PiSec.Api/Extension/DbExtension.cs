using Microsoft.EntityFrameworkCore;
using PiSec.Api.Repository;

namespace PiSec.Api.Extension
{
    public static class DbExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
