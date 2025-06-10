using TravelRouteAPI.Domain.Entities;
using TravelRouteAPI.Domain.Interfaces;
using TravelRouteAPI.Infrastructure.Persistence;

namespace TravelRouteAPI.Infrastructure.Repositories
{
    public class EfRouteRepository : IRouteRepository
    {
        private readonly RouteDbContext _context;
        public EfRouteRepository(RouteDbContext context) => _context = context;

        public IEnumerable<ChooseRoute> GetAll() => _context.Routes.ToList();
        public ChooseRoute? GetById(int id) => _context.Routes.Find(id);
        public ChooseRoute Add(ChooseRoute route) { _context.Routes.Add(route); _context.SaveChanges(); return route; }
        public void Update(ChooseRoute route) { _context.Routes.Update(route); _context.SaveChanges(); }
        public void Delete(int id) { var route = _context.Routes.Find(id); if (route != null) { _context.Routes.Remove(route); _context.SaveChanges(); } }
    }
}
