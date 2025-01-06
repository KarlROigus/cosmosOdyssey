namespace Domain;

public class RouteInfo
{
    
    public string Id { get; set; } = default!;
    public Planet From { get; set; } = default!;
    public Planet To { get; set; } = default!;
    public long Distance { get; set; }
}