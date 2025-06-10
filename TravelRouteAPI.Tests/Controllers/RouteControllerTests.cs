using Xunit;
using Moq;
using Bogus;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TravelRouteAPI.Controllers;
using TravelRouteAPI.Domain.Entities;
using TravelRouteAPI.Domain.Interfaces;
using TravelRouteAPI.TestUtils.Builders.Entity;

namespace TravelRouteAPI.Tests.Controllers;
    public class RouteControllerTests
    {
        private readonly Mock<IRouteRepository> _repoMock;
        private readonly RouteController _controller;
        private readonly ChooseRouteBuilder _builder;

        public RouteControllerTests()
        {
            _repoMock = new Mock<IRouteRepository>();
            _controller = new RouteController(_repoMock.Object);
            _builder = new ChooseRouteBuilder();
        }

        [Fact]
        public void GetAll_ReturnsOkWithRoutes()
        {
            var routes = _builder.BuildList(3);
            _repoMock.Setup(r => r.GetAll()).Returns(routes);

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(routes, okResult.Value);
        }

        [Fact]
        public void GetById_Found_ReturnsOk()
        {
            var route = _builder.Build();
            _repoMock.Setup(r => r.GetById(route.Id)).Returns(route);

            var result = _controller.GetById(route.Id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(route, okResult.Value);
        }

        [Fact]
        public void GetById_NotFound_ReturnsNotFound()
        {
        _repoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(It.IsAny<ChooseRoute>());

            var result = _controller.GetById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtAction()
        {
            var route = _builder.Build();
            _repoMock.Setup(r => r.Add(route)).Returns(route);

            var result = _controller.Create(route);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(route, created.Value);
            Assert.Equal(nameof(_controller.GetById), created.ActionName);
        }

        [Fact]
        public void Update_IdMismatch_ReturnsBadRequest()
        {
            var route = _builder.Build();

            var result = _controller.Update(route.Id + 1, route);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Update_NotFound_ReturnsNotFound()
        {
            var route = _builder.Build();
            _repoMock.Setup(r => r.GetById(route.Id)).Returns(It.IsAny<ChooseRoute>());

            var result = _controller.Update(route.Id, route);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_Valid_ReturnsNoContent()
        {
            var route = _builder.Build();
            _repoMock.Setup(r => r.GetById(route.Id)).Returns(route);

            var result = _controller.Update(route.Id, route);

            Assert.IsType<NoContentResult>(result);
            _repoMock.Verify(r => r.Update(route), Times.Once);
        }

        [Fact]
        public void Delete_NotFound_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(It.IsAny<ChooseRoute>());

            var result = _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Found_ReturnsNoContent()
        {
            var route = _builder.Build();
            _repoMock.Setup(r => r.GetById(route.Id)).Returns(route);

            var result = _controller.Delete(route.Id);

            Assert.IsType<NoContentResult>(result);
            _repoMock.Verify(r => r.Delete(route.Id), Times.Once);
        }
    }
