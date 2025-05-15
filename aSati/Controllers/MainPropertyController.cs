using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aSati.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "PropertyOwner")]
    public class MainPropertyController : ControllerBase
    {
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
    }
}
