namespace Domain;

public class Planet
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    
    public Planet(string id, string name)
    {
        Id = id;
        Name = name;
    }
}