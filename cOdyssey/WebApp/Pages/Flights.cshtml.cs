using System.Text.Json;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class Flights : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public ICollection<Leg> PossibleFlights { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)] public string Source { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string Destination { get; set; } = default!;
    [BindProperty(SupportsGet = true)] public string Company { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)] public string SortBy { get; set; } = default!;

    public Flights(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;

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

            var currentPriceListId = priceList!.Id;
            if (!_context.Apis.Any(each => each.ApiId == currentPriceListId))
            {
                RemoveOldestApiIfNeeded();

                await InsertNewApi(currentPriceListId, priceList);
            }
            
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

    private async Task InsertNewApi(string currentPriceListId, PriceList priceList)
    {
        var api = new API()
        {
            ApiId = currentPriceListId,
            ApiJsonString = JsonSerializer.Serialize(priceList),
            ValidUntil = priceList.ValidUntil
        };
        _context.Apis.Add(api);
        await _context.SaveChangesAsync();
    }

    private void RemoveOldestApiIfNeeded()
    {
        if (_context.Apis.Count() >= 15)
        {
            var oldestApi = _context.Apis.OrderBy(api => api.Id).FirstOrDefault();
            if (oldestApi != null)
            {
                _context.Apis.Remove(oldestApi);
            }
        }
    }
}