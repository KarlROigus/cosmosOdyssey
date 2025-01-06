using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class Flights : PageModel
{
    private readonly HttpClient _httpClient;

    public ICollection<Leg> PossibleFlights { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)] public string Source { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string Destination { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string Company { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)] public string SortBy { get; set; } = default!;

    public Flights(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task OnGetAsync()
    {
        await FetchDataFromApiToLegsCollection();

        PossibleFlights = PossibleFlights.Where(each => each.RouteInfo.From.Name == Source 
                                  && each.RouteInfo.To.Name == Destination).ToList();
        
        
        if (!string.IsNullOrEmpty(Company))
        {
            PossibleFlights = PossibleFlights.Where(leg =>
                leg.Providers!.Any(provider => provider.Company.Name.Contains(Company, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
        
        switch (SortBy)
        {
            case "Price":
            {
                foreach (var leg in PossibleFlights)
                {
                    if (leg.Providers != null)
                    {
                        leg.Providers = leg.Providers.OrderBy(provider => provider.Price).ToList();
                    }
                }

                break;
            }
            case "Time":
            {
                foreach (var leg in PossibleFlights)
                {
                    if (leg.Providers != null)
                    {
                        leg.Providers = leg.Providers.OrderBy(provider => (provider.FlightEnd - provider.FlightStart)).ToList();
                    }
                }

                break;
            }
        }
        
    }
    
    private async Task FetchDataFromApiToLegsCollection()
    {
        try
        {
            var priceList = await _httpClient.GetFromJsonAsync<PriceList>(
                "https://cosmosodyssey.azurewebsites.net/api/v1.0/TravelPrices"
            );
            
            if (priceList != null && priceList.Legs != null)
            {
                PossibleFlights = priceList.Legs;
                
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
        }
    }
}