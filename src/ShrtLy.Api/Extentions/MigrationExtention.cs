using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShrtLy.Infrastructure;

namespace ShrtLy.Api.Extentions
{
    public static class MigrationExtention
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ShrtLyContext>())
                {
                    appContext.Database.Migrate();
                }
            }
            return webApp;
        }
    }
}
