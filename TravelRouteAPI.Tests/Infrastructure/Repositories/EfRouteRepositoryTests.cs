using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRouteAPI.Domain.Entities;
using TravelRouteAPI.Infrastructure.Persistence;
using TravelRouteAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.InMemory;
using TravelRouteAPI.TestUtils.Builders.Entity;

namespace TravelRouteAPI.Tests.Infrastructure.Repositories;
public class EfRouteRepositoryTests
{
    private RouteDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<RouteDbContext>()
            .UseInMemoryDatabase(databaseName: "RouteDbTest");

        var context = new RouteDbContext(options.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public void Add_ShouldInsertRoute()
    {
        var context = CreateDbContext();
        var repo = new EfRouteRepository(context);

        var builder = new ChooseRouteBuilder();
        var mockRoutes = builder.Build();
        

        var result = repo.Add(mockRoutes);

        Assert.NotEqual(0, result.Id);        
    }

    [Fact]
    public void GetAll_ShouldReturnAllRoutes()
    {
        var context = CreateDbContext();
        var repo = new EfRouteRepository(context);
        var builder = new ChooseRouteBuilder();


        var mockRoutes = builder.BuildList(2);
        var Quatity = repo.GetAll().ToList().Count();

        context.Routes.AddRange(mockRoutes);
        context.SaveChanges();        
        var all = repo.GetAll().ToList();


        Assert.Equal(2, all.Count - Quatity);
    }

    [Fact]
    public void GetById_ShouldReturnCorrectRoute()
    {
        var context = CreateDbContext();
        var builder = new ChooseRouteBuilder();
        var mockRoutes = builder.Build();


        var added = context.Routes.Add(mockRoutes);
        context.SaveChanges();

        var repo = new EfRouteRepository(context);
        var result = repo.GetById(added.Entity.Id);

        Assert.Equal(added.Entity.Origin, result.Origin);
        Assert.Equal(added.Entity.Destination, result.Destination);
    }

    [Fact]
    public void Update_ShouldModifyRoute()
    {
        var context = CreateDbContext();
        var builder = new ChooseRouteBuilder();
        var mockRoutes = builder.Build();


        context.Routes.Add(mockRoutes);
        context.SaveChanges();

        var repo = new EfRouteRepository(context);
        mockRoutes.Price = 99;
        repo.Update(mockRoutes);

        var updated = context.Routes.Find(mockRoutes.Id);
        Assert.Equal(99, updated.Price);
    }

    [Fact]
    public void Delete_ShouldRemoveRoute()
    {
        var context = CreateDbContext();
        var builder = new ChooseRouteBuilder();
        var mockRoutes = builder.Build();
        context.Routes.Add(mockRoutes);
        context.SaveChanges();

        var repo = new EfRouteRepository(context);
        repo.Delete(mockRoutes.Id);

        var deleted = context.Routes.Find(mockRoutes.Id);

        Assert.Null(deleted);
    }
}

