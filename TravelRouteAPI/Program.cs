using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TravelRouteAPI.Application.Services;
using TravelRouteAPI.Domain.Interfaces;
using TravelRouteAPI.Infrastructure.Persistence;
using TravelRouteAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RouteDbContext>(options =>
    options.UseSqlite("Data Source=data/routes.db"));

// Add services to the container.
builder.Services.AddScoped<IRouteRepository, EfRouteRepository>();
builder.Services.AddScoped<IRouteCalculator, DijkstraRouteCalculator>();
builder.Services.AddScoped<RouteService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Travel Route API", Version = "v1" });
 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RouteDbContext>();
    db.Database.Migrate(); 
}

app.Run();
