using aSati.Data;
using aSati.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace aSati.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
//[Authorize(Roles = "PropertyOwner,Staff")]
public class LeaseController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public LeaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignTenantToUnit([FromBody] AssignLeaseDto dto)
    {
        var tenant = await _userManager.FindByEmailAsync(dto.TenantEmail);
        if (tenant == null)
            return BadRequest("Tenant not found");

        var unit = await _context.PropertyUnits.FirstOrDefaultAsync(u => u.Id == dto.PropertyUnitId);
        if (unit == null)
            return BadRequest("Unit not found");

        var lease = new Lease
        {
            PropertyUnitId = unit.Id,
            TenantId = tenant.Id,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = LeaseStatus.Active
        };

        _context.Leases.Add(lease);
        await _context.SaveChangesAsync();

        return Ok(lease);
    }
    [HttpGet("mine")]
    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> GetMyLeases()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var leases = await _context.Leases
            .Include(l => l.PropertyUnit)
                .ThenInclude(pu => pu.Property) // 👈 this is the fix
            .Where(l => l.TenantId == userId)
            .ToListAsync();

        return Ok(leases);
    }
}

public class AssignLeaseDto
{
    public Guid PropertyUnitId { get; set; }
    public string TenantEmail { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
}
