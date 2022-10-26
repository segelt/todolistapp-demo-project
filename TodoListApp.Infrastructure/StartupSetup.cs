using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoListApp.Application.Abstractions;
using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Application.Implementations;
using TodoListApp.Application.Implementations.Services;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Infrastructure.Data.Repo;

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

        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskService, TodoTaskService>();
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
