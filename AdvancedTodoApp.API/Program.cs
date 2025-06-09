using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.Services;
using AdvancedTodoApp.Persistence.Repositories;
using AdvancedTodoApp.Persistence;
using Microsoft.EntityFrameworkCore;

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
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
