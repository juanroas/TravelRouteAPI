using Xunit;
using Moq;
using TravelRouteAPI.Application.Services;
using TravelRouteAPI.Domain.Interfaces;
using TravelRouteAPI.TestUtils.Builders.Entity;
using TravelRouteAPI.Application.DTOs;

namespace TravelRouteAPI.Tests.Application.Services;
public class RouteServiceTests
{
    private readonly Mock<IRouteRepository> _repositoryMock;
    private readonly Mock<IRouteCalculator> _calculatorMock;
    private readonly RouteService _service;

    public RouteServiceTests()
    {
        _repositoryMock = new Mock<IRouteRepository>();
        _calculatorMock = new Mock<IRouteCalculator>();
        _service = new RouteService(_repositoryMock.Object, _calculatorMock.Object);
    }

    [Fact]
    public void FindRoute_ShouldCallCalculatorWithRoutes()
    {
        // Arrange
        var builder = new ChooseRouteBuilder();
        var mockRoutes = builder.BuildList(3);

        _repositoryMock.Setup(r => r.GetAll()).Returns(mockRoutes);

        _calculatorMock.Setup(c => c.CalculateBestRoute("GRU", "CDG", mockRoutes))
                       .Returns(new BestRouteResult { Answer = "TEST-ANSWER" });

        // Act
        var result = _service.FindRoute("GRU", "CDG");

        // Assert
        Assert.Equal("TEST-ANSWER", result.Answer);
        _repositoryMock.Verify(r => r.GetAll(), Times.Once);
        _calculatorMock.Verify(c => c.CalculateBestRoute("GRU", "CDG", mockRoutes), Times.Once);
    }
}

