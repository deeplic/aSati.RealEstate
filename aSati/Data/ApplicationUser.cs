using aSati.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace aSati.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<MainProperty>? PropertiesOwned { get; set; }
        public List<Lease>? Leases { get; set; }
    }

}
