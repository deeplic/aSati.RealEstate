using aSati.Data;
using aSati.Shared.Models;
using AutoMapper;
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
        private readonly ILogger<PropertyController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public PropertyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<PropertyController> logger)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
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

            var result = _mapper.Map<List<MainPropertyDto>>(properties);
            return Ok(result);
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
        public async Task<ActionResult<List<UnitDto>>> GetUnits(Guid propertyId)
        {
            var property = await _context.Properties
                .Include(p => p.Units)
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
                return NotFound();
            var result = _mapper.Map<List<UnitDto>>(property.Units);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MainPropertyDto>> GetProperty(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null) return NotFound();
            var dto =new MainPropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                Address = property.Address,
                Units = property.Units.Select(u => new UnitDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Leases = u.Leases.Select(l => new LeaseDto
                    {
                        Id = l.Id,
                        StartDate = l.StartDate,
                        EndDate = l.EndDate
                    }).ToList()
                }).ToList(),
                City = property.City,
                Country = property.Country
            };

            return dto;
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
