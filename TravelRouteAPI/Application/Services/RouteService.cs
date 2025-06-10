using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using TravelRouteAPI.Application.DTOs;
using TravelRouteAPI.Domain.Interfaces;

namespace TravelRouteAPI.Application.Services
{
    public class RouteService(IRouteRepository repository, IRouteCalculator calculator)
    {
        private readonly IRouteRepository _repository = repository;
        private readonly IRouteCalculator _calculator = calculator;

        public BestRouteResult FindRoute(string origin, string destination)
        {
            var routes = _repository.GetAll();
            return _calculator.CalculateBestRoute(origin, destination, routes);
        }

    }
}
