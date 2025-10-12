using Microsoft.AspNetCore.Identity;

namespace EventHub.Data
{
    public class SeedData
    {
        private static readonly string[] Roles = new[] { "User", "Organizer", "Admin" };

        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
