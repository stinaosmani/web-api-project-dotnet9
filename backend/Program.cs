using Application.Configuration.Interceptors;
using backend.src.Application.Configuration.DependencyInjection;
using backend.src.Infrastructure.Middleware;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "xTDB API", Version = "v1" });

    //options.AddServer(new OpenApiServer
    //{
    //    Url = "https://localhost:7071",
    //    Description = "Development HTTPS"
    //});

    //options.AddServer(new OpenApiServer
    //{
    //    Url = "http://localhost:30000",
    //    Description = "Docker container"
    //});
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register services via DI installer
builder.Services.InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
    var timing = sp.GetRequiredService<DbOperationTimingInterceptor>();

    options
        .UseOracle(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            oracle => oracle.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
        )
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)     
        .AddInterceptors(interceptor, timing);
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPlayground v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
