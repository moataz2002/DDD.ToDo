using DDD.ToDo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DDD.ToDo.Application.Services.Todo;
using DDD.ToDo.Infrastructure.Repositories.Todo;

namespace DDD.ToDo.WebAPI.Extensions
{
    public static class BuilderExtensions
    {
        // add db context
        public static WebApplicationBuilder AddBuilderRegistration(this WebApplicationBuilder builder)
        {
            builder.RegisterDbContexts();
            builder.CreateAndMigratedDatabase();
            builder.AddServicesRegistration();

            return builder;
        }

        public static WebApplicationBuilder RegisterDbContexts(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return builder;
        }

        public static WebApplicationBuilder AddServicesRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<ITodoService, TodoService>();
            return builder;
        }

        public static WebApplicationBuilder CreateAndMigratedDatabase(this WebApplicationBuilder builder)
        {
            using var serviceScope = builder.Services.BuildServiceProvider().CreateScope();
            try
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context!.Database.Migrate();
                context.Database.EnsureCreated();
            }
            catch
            {
                Debug.WriteLine("Error while migrating database");
            }

            return builder;
        }
    }
}
