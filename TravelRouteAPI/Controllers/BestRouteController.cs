using Microsoft.AspNetCore.Mvc;
using TravelRouteAPI.Application.DTOs;
using TravelRouteAPI.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelRouteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestRouteController : ControllerBase
    {
        private readonly RouteService _routeService;

        public BestRouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public ActionResult<BestRouteResult> Get([FromQuery] string route)
        {
            if (string.IsNullOrEmpty(route))
                return BadRequest("Please, informe origin and destination.");

            var originDestination = route.Split('-', 2);

            if (originDestination.Length < 2 || string.IsNullOrEmpty(originDestination[0]) || string.IsNullOrEmpty(originDestination[1]))
                return BadRequest("Both origin and Destination are required.");

            var origin = originDestination[0];
            var destination = originDestination[1];            

            return Ok(_routeService.FindRoute(origin.ToUpper(), destination.ToUpper()));
        }
    }
}
