using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRouteAPI.Application.Services;
using TravelRouteAPI.Domain.Entities;
using TravelRouteAPI.TestUtils.Builders.Entity;

namespace TravelRouteAPI.Tests.Application.Services;
public class DijkstraRouteCalculatorTests
{
    private readonly DijkstraRouteCalculator _calculator;
    private readonly ChooseRouteBuilder _builder;

    public DijkstraRouteCalculatorTests()
    {
        _calculator = new DijkstraRouteCalculator();
        _builder = new ChooseRouteBuilder();
    }

    [Fact]
    public void CalculateBestRoute_WithDirectPath_ReturnsDirectPath()
    {
        var routes = _builder.CustomeChooseRoute("GRU", "CDG", 75);

        var result = _calculator.CalculateBestRoute("GRU", "CDG", routes);

        Assert.Equal("GRU - CDG ao custo de $75", result.Answer);
    }

    [Fact]
    public void CalculateBestRoute_WithMultiplePaths_ReturnsCheapestPath()
    {
        var routes = _builder.ListChooseRoutes();

        var result = _calculator.CalculateBestRoute("GRU", "CDG", routes);

        Assert.Equal("GRU - BRC - SCL - ORL - CDG ao custo de $40", result.Answer);
    }

    [Fact]
    public void CalculateBestRoute_WithNoConnection_ReturnsNoRouteFound()
    {
        var routes = _builder.BuildList(5); 

        var result = _calculator.CalculateBestRoute("GRU", "ZZZ", routes);

        Assert.Equal("No route Found", result.Answer);
    }

    [Fact]
    public void CalculateBestRoute_WhenOriginEqualsDestination_ReturnsZeroPath()
    {
        var routes = _builder.BuildList(3);
        var origin = routes[0].Origin;

        var result = _calculator.CalculateBestRoute(origin, origin, routes);

        Assert.Contains(origin, result.Answer);
    }
}

