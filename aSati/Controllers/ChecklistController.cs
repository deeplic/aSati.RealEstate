using aSati.Data;
using aSati.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace aSati.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Tenant")] // Only tenants can submit
public class ChecklistController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public ChecklistController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("submit")]
    //[Authorize(Roles = "Tenant")]
    public async Task<IActionResult> SubmitChecklist([FromQuery] Guid leaseId, [FromBody] List<PropertyChecklistItem> items)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var lease = await _context.Leases.Include(l => l.ChecklistItems).FirstOrDefaultAsync(l => l.Id == leaseId);

        if (lease == null || lease.TenantId != userId)
            return Unauthorized("You are not allowed to update this lease.");

        lease.ChecklistItems = items;
        await _context.SaveChangesAsync();

        return Ok();
    }
    [HttpGet("{leaseId}/checklist")]
   // [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> GetChecklistForLease(Guid leaseId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var lease = await _context.Leases
            .Include(l => l.ChecklistItems)
            .FirstOrDefaultAsync(l => l.Id == leaseId && l.TenantId == userId);

        if (lease == null)
            return NotFound("Lease not found or access denied.");

        var checklist = _mapper.Map<List<ChecklistItemDto>>(lease.ChecklistItems);
        return Ok(checklist);
    }
}