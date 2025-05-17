using aSati.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "PropertyOwner,Staff")]
public class LeaseReviewController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public LeaseReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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
    [HttpGet("{leaseId}")]
    public async Task<ActionResult<LeaseReviewDto>> GetLeaseReview(Guid leaseId)
    {
        var lease = await _context.Leases
            .Include(l => l.PropertyUnit)
            .Include(l => l.ChecklistItems)
            .FirstOrDefaultAsync(l => l.Id == leaseId);

        if (lease == null)
            return NotFound("Lease not found");

        var tenantUser = await _userManager.FindByIdAsync(lease.TenantId);
        var tenantName = tenantUser?.UserName ?? "Unknown";

        var checklistCompleted = lease.ChecklistItems.All(ci => ci.Status == ChecklistStatus.Completed);

        var dto = new LeaseReviewDto
        {
            LeaseId = lease.Id,
            UnitName = lease.PropertyUnit?.Name ?? "N/A",
            TenantName = tenantName,
            StartDate = lease.StartDate,
            EndDate = lease.EndDate ?? DateTime.MaxValue,
            ChecklistCompleted = checklistCompleted,
            OwnerComment = lease.OwnerComment // If you later add it to the model
        };

        return Ok(dto);
    }
    [HttpPost("review/item/{id:guid}/approve")]
    public async Task<IActionResult> ApproveItem(Guid id)
    {
        var item = await _context.PropertyChecklistItems.FindAsync(id);
        if (item is null) return NotFound();

        item.Status = ChecklistStatus.Approve;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("review/item/{id:guid}/request-changes")]
    public async Task<IActionResult> RequestChanges(Guid id)
    {
        var item = await _context.PropertyChecklistItems.FindAsync(id);
        if (item is null) return NotFound();

        item.Status = ChecklistStatus.NeedsReview;
        await _context.SaveChangesAsync();
        return Ok();
    }

}