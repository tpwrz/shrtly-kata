using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShrtLy.Contracts;
using ShrtLy.Infrastructure;
using System;

namespace ShrtLy.Api.Extencions
{
    public static class DatabaseConfigExtention
    {
        private const string MigrationsAssemblyName = "ShrtLy.Infrastructure";

        public static void ConfigureDatabaseServices(this WebApplicationBuilder builder)
        {
            builder.SetupRepositories();
            builder.SetupDatabase();
        }

        private static void SetupRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(ILinksRepository), typeof(LinksRepository));
        }

        private static void SetupDatabase(this WebApplicationBuilder builder)
        {
            //var connectionString = "User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=Shrtly;Integrated Security=true;";
            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    throw new InvalidOperationException("PG_DB_CONNECTION is not set.");
            //}

            //builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ShrtLyContext>(opt =>
            //{
            //    opt.UseNpgsql(connectionString,
            //        x => x.MigrationsAssembly(MigrationsAssemblyName));
            //});

            var connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("DB_CONNECTION is not set.");
            }

            builder.Services.AddDbContext<ShrtLyContext>(opt =>
            {
                opt.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly(MigrationsAssemblyName));
            });
        }
    }
}
