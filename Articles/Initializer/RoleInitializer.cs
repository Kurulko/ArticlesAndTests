using Articles.Models;
using Articles.Models.DbModels;
using Microsoft.AspNetCore.Identity;

namespace Articles.Initializer
{
    public class RoleInitializer
    {
        static async Task<bool> HasRoleAsync(RoleManager<IdentityRole> roleManager, string name)
            => await roleManager.FindByNameAsync(name) is not null;
        static async Task CreateRoleAsync(RoleManager<IdentityRole> roleManager, string name)
            => await roleManager.CreateAsync(new IdentityRole(name));
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };
            foreach (string role in roles)
                if (!await HasRoleAsync(roleManager, role))
                    await CreateRoleAsync(roleManager, role);

            string adminName = "Admin";
            if (await userManager.FindByNameAsync(adminName) is null)
            {
                string password = "QEhvosH55_jvkfs68bslIEk";
                User admin = new() { UserName = adminName, DateTime = DateTime.Now };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user: admin, role: adminName);
            }
        }
    }
}
