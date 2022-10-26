﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoListApp.Infrastructure.Data;

namespace TodoListApp.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Transient);
        }

        public static void PrepareDatabase(AppDbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
            dbContext.Database.EnsureCreated();
        }
    }
}
