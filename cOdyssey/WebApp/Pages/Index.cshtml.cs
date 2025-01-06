using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Planet> Planets { get; set; } = new()
        {
            new("Earth", "Earth"),
            new("Jupiter", "Jupiter"),
            new("Mars", "Mars"),
            new("Venus", "Venus"),
            new("Mercury", "Mercury"),
            new("Saturn", "Saturn"),
            new("Uranus", "Uranus"),
            new("Neptune", "Neptune"),
        };

        [BindProperty(SupportsGet = true)] public string Source { get; set; } = default!;
        [BindProperty(SupportsGet = true)] public string Destination { get; set; } = default!;
        

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(Destination))
            {
                return RedirectToPage("./Flights", new { Source, Destination });
            }

            return Page();
        }
    }
}