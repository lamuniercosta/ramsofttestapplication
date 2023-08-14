using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RamSoftTaskApplication.Data;
using RamSoftTaskApplication.Data.Repositories;
using RamSoftTaskApplication.Services.Tasks;

namespace RamSoftTaskApplication.API;

public class Startup
{
    public static WebApplication InitiateApp(string[] parameters)
    {
        var builder = WebApplication.CreateBuilder(parameters);

        builder.Logging.ClearProviders();

        ConfigureServices(builder.Services);

        var app = builder.Build();

        Configure(app);

        return app;
    }
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITaskService, TaskService>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("TaskManagementInMemoryDb");
        });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(configuration =>
        {
            configuration.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Task Manager API",
                Version = "v1"
            });

            configuration.CustomSchemaIds(type => type.ToString());
        });
        services.AddSwaggerGenNewtonsoftSupport();
    }

    private static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        else
        {
            app.UseHsts();
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
        }

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(configuration =>
        {
            configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API v1");
            configuration.RoutePrefix = "swagger";
            configuration.InjectStylesheet("/SwaggerUi.css");
        });

        app.MapControllers();
        app.UseCors(builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000", "https://localhost:3000")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
 
            }
        );
    }
}