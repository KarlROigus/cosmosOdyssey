namespace Domain;

public class API
{
    public int Id { get; set; }
    public string ApiId { get; set; } = default!;

    public DateTime ValidUntil { get; set; } = default!;
    public string ApiJsonString { get; set; } = default!;
}