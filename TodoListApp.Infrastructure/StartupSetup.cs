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
    /// <summary>
    /// Class that manages the setup of all types of dependencies. Any external sources (database, redis,
    /// clients etc.) should be configured and prepared in this class.
    /// </summary>
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

        /// <summary>
        /// Applies any missing migrations to the database, and ensures that all tables are created.
        /// </summary>
        /// <param name="dbContext"></param>
        public static void PrepareDatabase(AppDbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
            dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Manages the registration of dependencies.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskService, TodoTaskService>();
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
