using Moq;
using Microsoft.AspNetCore.Mvc;
using TravelRouteAPI.Controllers;
using TravelRouteAPI.Application.Services;
using TravelRouteAPI.Application.DTOs;
using TravelRouteAPI.Domain.Interfaces;
using TravelRouteAPI.Domain.Entities;

namespace TravelRouteAPI.Tests.Controllers
{
    public class BestRouteControllerTests
    {
        private readonly Mock<IRouteRepository> _repositoryMock;
        private readonly Mock<IRouteCalculator> _calculatorMock;
        private readonly RouteService _serviceMock;
        private readonly BestRouteController _controller;

        public BestRouteControllerTests()
        {
            _repositoryMock = new Mock<IRouteRepository>();
            _calculatorMock = new Mock<IRouteCalculator>();
            _serviceMock = new RouteService(_repositoryMock.Object, _calculatorMock.Object);
            _controller = new BestRouteController(_serviceMock);
        }

        [Fact]
        public void Get_NullOrEmptyRoute_ReturnsBadRequest()
        {
            var result1 = _controller.Get(null);
            var result2 = _controller.Get(string.Empty);

            var badRequest1 = Assert.IsType<BadRequestObjectResult>(result1.Result);
            var badRequest2 = Assert.IsType<BadRequestObjectResult>(result2.Result);
            

            Assert.Equal("Please, informe origin and destination.", badRequest1.Value?.ToString());
            Assert.Equal("Please, informe origin and destination.", badRequest2.Value?.ToString());
        }

        [Theory]
        [InlineData("ABC")]
        [InlineData("ABC-")]
        [InlineData("-DEF")]
        public void Get_InvalidRouteFormat_ReturnsBadRequest(string route)
        {
            var result = _controller.Get(route);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Both origin and Destination are required.", badRequest.Value);
        }

        [Fact]
        public void Get_ValidRoute_ReturnsOkWithResult()
        {
            var route = "GRU-CDG";
            var expectedResult = new BestRouteResult { Answer = "GRU - CDG ao custo de $" + 100 };

            _repositoryMock.Setup(r => r.GetAll()).Returns(new List<ChooseRoute>());
            
            _calculatorMock
                .Setup(c => c.CalculateBestRoute("GRU", "CDG", It.IsAny<IEnumerable<ChooseRoute>>()))
                .Returns(expectedResult);

            var result = _controller.Get(route);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResult, okResult.Value);
            _calculatorMock.Verify(s => s.CalculateBestRoute("GRU", "CDG", It.IsAny<IEnumerable<ChooseRoute>>()), Times.Once);
        }
    }    
}
