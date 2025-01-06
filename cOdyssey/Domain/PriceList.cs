namespace Domain;

public class PriceList
{
    public string Id { get; set; } = default!;
    public DateTime ValidUntil { get; set; }
    public ICollection<Leg>? Legs { get; set; }
}