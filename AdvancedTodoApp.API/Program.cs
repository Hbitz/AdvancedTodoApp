using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.Services;
using AdvancedTodoApp.Persistence.Repositories;
using AdvancedTodoApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using AdvancedTodoApp.Persistence.Context;

namespace AdvancedTodoApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Add DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("AdvancedTodoApp.Persistence"))); // Tells EF core where the migrations are located

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
