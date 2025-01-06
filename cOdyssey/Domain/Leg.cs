namespace Domain;

public class Leg
{
    public string Id { get; set; } = default!;
    public RouteInfo RouteInfo { get; set; } = default!;
    public ICollection<Provider>? Providers { get; set; }
}