namespace TravelRouteAPI.Domain.Entities
{
    public class ChooseRoute
    {
        public int Id { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
