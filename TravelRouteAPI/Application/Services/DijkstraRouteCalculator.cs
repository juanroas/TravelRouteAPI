using TravelRouteAPI.Application.DTOs;
using TravelRouteAPI.Domain.Entities;
using TravelRouteAPI.Domain.Interfaces;

namespace TravelRouteAPI.Application.Services;
public class DijkstraRouteCalculator : IRouteCalculator
{
    public BestRouteResult CalculateBestRoute(string origin, string destination, IEnumerable<ChooseRoute> routes)
    {
        var airports = routes.SelectMany(r => new[] { r.Origin, r.Destination })
                                       .Distinct()
                                       .ToList();

        //Grafo
        Dictionary<string, Dictionary<string, decimal>> grafo = new();
        foreach (var airport in airports)
        {
            grafo[airport] = new Dictionary<string, decimal>();
        }

        foreach (var route in routes)
        {
            grafo[route.Origin][route.Destination] = route.Price;
        }


        //Unvisited, destination, previous
        Dictionary<string, decimal> distancia = new();
        Dictionary<string, string> previous = new();
        HashSet<string> unvisited = new();

        foreach (var airport in airports)
        {
            distancia[airport] = airport == origin ? 0 : decimal.MaxValue;
            unvisited.Add(airport);
        }

        while (unvisited.Count() > 0)
        {
            string current = unvisited.OrderBy(d => distancia[d]).First();
            if (current == destination || distancia[current] == decimal.MaxValue)
                break;

            unvisited.Remove(current);

            foreach (var neighbor in grafo[current])
            {
                var alt = distancia[current] + neighbor.Value;
                if (alt < distancia[neighbor.Key])
                {
                    distancia[neighbor.Key] = alt;
                    previous[neighbor.Key] = current;
                }
            }
        }

        //Answer
        if (!previous.ContainsKey(destination) && origin != destination)
            return new BestRouteResult { Answer = "No route Found" };

        List<string> path = new();
        string step = destination;
        while (!string.IsNullOrEmpty(step))
        {
            path.Add(step);
            previous.TryGetValue(step, out step);
            if (step == origin)
            {
                path.Add(origin);
                break;
            }
        }

        path.Reverse();

        return new BestRouteResult
        {
            Answer = string.Join(" - ", path) + " ao custo de $" + distancia[destination]
        };
    }
}

