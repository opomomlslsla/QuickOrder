using Microsoft.EntityFrameworkCore;
using QuickOrder.Application;
using QuickOrder.Infrastructure;
using QuickOrder.Infrastructure.Data;
using QuickOrder.Server.Middleware;
namespace QuickOrder.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigureInfrastructureServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("reactFrontEnd",
                    policy =>
                    {
                        policy.WithOrigins(
                                "http://localhost:5173",    // Vite локально
                                "http://localhost:80",      // Docker фронтенд
                                "http://localhost")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials();
                    });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
                if(!dbContext.Database.GetAppliedMigrations().Any())
                    dbContext.Database.Migrate();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors("reactFrontEnd");


            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
