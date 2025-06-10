using TravelRouteAPI.Application.DTOs;
using TravelRouteAPI.Domain.Entities;

namespace TravelRouteAPI.Domain.Interfaces;

    public interface IRouteCalculator
    {
        BestRouteResult CalculateBestRoute(string origin, string destination, IEnumerable<ChooseRoute> routes);
    }

