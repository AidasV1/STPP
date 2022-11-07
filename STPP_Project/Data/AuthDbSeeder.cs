using Microsoft.AspNetCore.Identity;
using STPP_Project.Auth.Model;

namespace STPP_Project.Data
{
    public class AuthDbSeeder
    {
        private readonly UserManager<ProjectRestUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthDbSeeder(UserManager<ProjectRestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddAdminUser()
        {
            var newAdminUser = new ProjectRestUser()
            {
                UserName = "admin",
                Email = "admin@a.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "Asdasd!1");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, ProjectRoles.All);
                }
            }
        }

        private async Task AddDefaultRoles()
        {
            foreach (var role in ProjectRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
