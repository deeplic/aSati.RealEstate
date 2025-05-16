using aSati.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "PropertyOwner,Staff")]
public class LeaseReviewController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LeaseReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("leases-with-checklists")]
    public async Task<IActionResult> GetLeases()
    {
        var leases = await _context.Leases
            .Include(l => l.ChecklistItems)
            .Include(l => l.PropertyUnit)
            .ThenInclude(pu => pu.Property)
            .ToListAsync();

        return Ok(leases);
    }
}