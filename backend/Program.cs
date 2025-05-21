using Application.Configuration.Interceptors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register interceptor
builder.Services.AddScoped<UpdateAuditableEntitiesInterceptor>();
builder.Services.AddHttpContextAccessor();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>();

    options.UseOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        oracle => oracle.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
    )
    .AddInterceptors(interceptor);
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Swagger pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
