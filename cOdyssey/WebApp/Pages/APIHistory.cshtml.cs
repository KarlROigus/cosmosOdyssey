using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class APIHistory : PageModel
{
    
    private readonly AppDbContext _context;
        
    public APIHistory(AppDbContext context)
    {
        _context = context;
    }
    
    public ICollection<API> APIList { get; set; } = default!;
    
    public async Task OnGet()
    {
        APIList = await _context.Apis.ToListAsync();
    }
}