using aSati.Data;
using aSati.Shared;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Superuser", "SystemAdmin", "PropertyOwner", "Staff", "Tenant" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Superuser
        string superEmail = "superuser@asati.com";
        var superUser = await userManager.FindByEmailAsync(superEmail);
        if (superUser == null)
        {
            var user = new ApplicationUser { UserName = superEmail, Email = superEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, "Admin123!"); // change password later
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Superuser");
            }
        }
    }
}
