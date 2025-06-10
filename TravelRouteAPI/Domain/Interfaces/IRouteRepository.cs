using TravelRouteAPI.Domain.Entities;

namespace TravelRouteAPI.Domain.Interfaces
{
    public interface IRouteRepository
    {
        IEnumerable<ChooseRoute> GetAll();
        ChooseRoute? GetById(int id);
        ChooseRoute Add(ChooseRoute route);
        void Update(ChooseRoute route);
        void Delete(int id);
    }
}
