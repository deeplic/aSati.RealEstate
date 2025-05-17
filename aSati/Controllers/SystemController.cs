// SystemController.cs
using aSati.Data;
using aSati.Shared.DTOs;
using aSati.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static aSati.Client.Pages.Dashboard.SystemAdminDashboard;

[Authorize(Roles = "SystemAdmin")]
[ApiController]
[Route("api/system")]
public class SystemController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public SystemController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var users = await _userManager.Users.ToListAsync();

        var result = new SystemAdminDashboardDto
        {
            TotalUsers = users.Count,
            PropertyOwners = users.Count(u => u.Roles.Contains("PropertyOwner")),
            StaffMembers = users.Count(u => u.Roles.Contains("Staff")),
            Tenants = users.Count(u => u.Roles.Contains("Tenant")),
            TotalProperties = await _context.Properties.CountAsync(),
            TotalUnits = await _context.PropertyUnits.CountAsync(),
            ActiveLeases = await _context.Leases.CountAsync(l => l.Status == LeaseStatus.Active)
        };

        return Ok(result);
    }
}