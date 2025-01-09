using System.Globalization;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class ReserveFlight : PageModel
    {
        private readonly AppDbContext _context;
        
        public ReserveFlight(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)] 
        public string Source { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Destination { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string CompanyName { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public decimal Price { get; set; }

        [BindProperty(SupportsGet = true)] public string FlightStart { get; set; } = default!;

        [BindProperty(SupportsGet = true)] public string FlightEnd { get; set; } = default!;
        
        [BindProperty(SupportsGet = true)] public string ValidUntil { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int Distance { get; set; }

        [BindProperty]
        public string FirstName { get; set; } = default!;

        [BindProperty]
        public string LastName { get; set; } = default!;

        public string Error { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (DateTime.Now > DateTime.Parse(ValidUntil))
            {
                Error = "The current price list expired. Please start again from the main menu!";
                return Page();
            }
            
            var reservation = new Reservation
            {
                FirstName = FirstName,
                LastName = LastName,
                Source = Source,
                Destination = Destination,
                CompanyName = CompanyName,
                Price = Price,
                FlightStart = DateTime.Parse(FlightStart),
                FlightEnd = DateTime.Parse(FlightEnd),
                Distance = Distance
            };
            
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Success");

        }
        
    }