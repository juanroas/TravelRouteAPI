using Microsoft.AspNetCore.Mvc;
using TravelRouteAPI.Domain.Entities;
using TravelRouteAPI.Domain.Interfaces;

namespace TravelRouteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        
        private readonly IRouteRepository _repository;
        public RouteController(IRouteRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ChooseRoute>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ChooseRoute> GetById(int id)
        {
            var route = _repository.GetById(id);
            if (route == null)
                return NotFound();

            return Ok(route);
        }


        [HttpPost]
        public ActionResult<ChooseRoute> Create(ChooseRoute route)
        {
            var createRoute = _repository.Add(route);
            return CreatedAtAction(nameof(GetById), new { id = createRoute.Id }, createRoute);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ChooseRoute route)
        {
            if (id != route.Id)
                return BadRequest();

            var existinRoute = _repository.GetById(id);
            if (existinRoute == null)
                return NotFound();

            _repository.Update(route);
            return NoContent();

        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var route = _repository.GetById(id);
            if (route == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
