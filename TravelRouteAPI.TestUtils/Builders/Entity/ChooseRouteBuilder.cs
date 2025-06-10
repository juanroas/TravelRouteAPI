using Bogus;
using TravelRouteAPI.Domain.Entities;

namespace TravelRouteAPI.TestUtils.Builders.Entity
{
    public class ChooseRouteBuilder
    {
        private readonly Faker<ChooseRoute> _faker;

        public ChooseRouteBuilder()
        {
            _faker = new Faker<ChooseRoute>()
                //.RuleFor(r => r.Id, f => f.IndexFaker + 100)
                .RuleFor(r => r.Origin, f => f.Random.String2(3).ToUpper())
                .RuleFor(r => r.Destination, f => f.Random.String2(3).ToUpper())
                .RuleFor(r => r.Price, f => f.Random.Decimal(5, 100));
        }

        public List<ChooseRoute> ListChooseRoutes()
        {
            return new List<ChooseRoute>
                {
                    new ChooseRoute { Origin = "GRU", Destination = "BRC", Price = 10 },
                    new ChooseRoute { Origin = "BRC", Destination = "SCL", Price = 5 },
                    new ChooseRoute { Origin = "SCL", Destination = "ORL", Price = 20 },
                    new ChooseRoute { Origin = "ORL", Destination = "CDG", Price = 5 },
                    new ChooseRoute { Origin = "GRU", Destination = "CDG", Price = 75 }
                };
        }

        public ChooseRoute Build() => _faker.Generate();
        public List<ChooseRoute> BuildList(int count) => _faker.Generate(count);

        public IEnumerable<ChooseRoute> CustomeChooseRoute(string origin, string destination, decimal price)
        {
            return new List<ChooseRoute>
            {
                new ChooseRoute
                {
                    Origin = origin,
                    Destination = destination,
                    Price = price
                }

            };
        }
    }
}
