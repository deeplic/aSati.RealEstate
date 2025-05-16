using aSati.Data;
using aSati.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aSati.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "PropertyOwner")]
    public class PropertyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public PropertyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMyProperties()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var userId = user.Id;

            var properties = await _context.Properties
                .Where(p => p.OwnerId == userId)
                .Include(p => p.Units)
                    .ThenInclude(u => u.Leases)
                .ToListAsync();

            return Ok(properties);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProperty(MainProperty model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            model.OwnerId = user.Id;

            _context.Properties.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetProperties()
        {
            // For now, return mock data
            var properties = new[] {
                new { Id = 1, Name = "Test Property", Location = "City A" },
                new { Id = 2, Name = "Sample House", Location = "City B" }
            };

            return Ok(properties);
        }
        [HttpGet("{propertyId}/units")]
        public async Task<ActionResult<List<PropertyUnit>>> GetUnits(Guid propertyId)
        {
            var property = await _context.Properties
                .Include(p => p.Units)
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
                return NotFound();

            return Ok(property.Units);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MainProperty>> GetProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();
            return property;
        }
        [HttpPost("/api/unit")]
        public async Task<IActionResult> AddUnit([FromBody] PropertyUnit unit)
        {
            if (!await _context.Properties.AnyAsync(p => p.Id == unit.PropertyId))
                return BadRequest("Invalid property ID.");

            _context.PropertyUnits.Add(unit);
            await _context.SaveChangesAsync();

            return Ok(unit);
        }

    }
}
