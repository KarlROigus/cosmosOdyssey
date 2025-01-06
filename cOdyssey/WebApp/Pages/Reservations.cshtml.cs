using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Reservations : PageModel
{
    private readonly AppDbContext _context;
        
    public Reservations(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Reservation> ReservationsList { get; set; } = default!;

    
    public async Task OnGetAsync()
    {
        
        ReservationsList = await _context.Reservations.ToListAsync();
    }
}