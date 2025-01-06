namespace Domain;

public class Reservation
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string Destination { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
    public int Distance { get; set; }
}