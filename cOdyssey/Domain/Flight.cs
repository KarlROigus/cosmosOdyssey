namespace Domain;

public class Flight
{
    public string Id { get; set; } = default!;
    public Planet? Source { get; set; }
    public Planet? Destination { get; set; }
    public Company? Company { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public decimal Price { get; set; }
    public decimal Distance { get; set; }

    public Flight(string id, Planet source, Planet destination, Company company, DateTime start, DateTime finish,
        decimal price, decimal distance)
    {
        Id = id;
        Source = source;
        Destination = destination;
        Company = company;
        Start = start;
        Finish = finish;
        Price = price;
        Distance = distance;
    }
    
}