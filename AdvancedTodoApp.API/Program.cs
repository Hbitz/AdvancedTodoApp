using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.Services;
using AdvancedTodoApp.Persistence.Repositories;
using AdvancedTodoApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using AdvancedTodoApp.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using AdvancedTodoApp.Application.Common.Behaviors;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using MediatR;
using AdvancedTodoApp.Application.Interfaces.Auth;
using AdvancedTodoApp.Infrastructure.Auth;

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
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Register MediatR
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly);
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
