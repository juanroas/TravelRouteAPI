using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TravelRouteAPI.Domain.Entities;

namespace TravelRouteAPI.Infrastructure.Persistence;
public class RouteDbContext : DbContext
{
    public DbSet<ChooseRoute> Routes { get; set; }
    public RouteDbContext(DbContextOptions<RouteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChooseRoute>().HasData(
            new ChooseRoute { Id = 1, Origin = "GRU", Destination = "BRC", Price = 10 },
            new ChooseRoute { Id = 2, Origin = "BRC", Destination = "SCL", Price = 5 },
            new ChooseRoute { Id = 3, Origin = "GRU", Destination = "CDG", Price = 75 },
            new ChooseRoute { Id = 4, Origin = "GRU", Destination = "SCL", Price = 20 },
            new ChooseRoute { Id = 5, Origin = "GRU", Destination = "ORL", Price = 56 },
            new ChooseRoute { Id = 6, Origin = "ORL", Destination = "CDG", Price = 5 },
            new ChooseRoute { Id = 7, Origin = "SCL", Destination = "ORL", Price = 20 }
        );
    }
}

