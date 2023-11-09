using Microsoft.AspNetCore.Identity;
using WebApplication3.ViewModel;

namespace WebApplication3.Repositories
{
    public class UserRoleRepo
    {
        private readonly IServiceProvider serviceProvider;

        public UserRoleRepo(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        // Assign a role to a user.
        public async Task<bool> AddUserRole(string email, string roleName)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                if (await UserManager.IsInRoleAsync(user, roleName))
                {
                    // The role assignment already exists
                    return false;
                }

                await UserManager.AddToRoleAsync(user, roleName);
                return true;
            }
            else
            {
                return false; // User not found
            }
        }

        // Remove role from a user.
        public async Task<bool> RemoveUserRole(string email, string roleName)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                if (await UserManager.IsInRoleAsync(user, roleName))
                {
                    await UserManager.RemoveFromRoleAsync(user, roleName);
                    return true;
                }
                else
                {
                    return false; // Role not assigned
                }
            }
            return false; // User not found
        }

        // Get all roles of a specific user.
        public async Task<List<RoleVM>> GetUserRoles(string email)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                var roles = await UserManager.GetRolesAsync(user);

                List<RoleVM> roleVMObjects = new List<RoleVM>();
                foreach (string role in roles)
                {
                    roleVMObjects.Add(new RoleVM()
                    {
                        Id = role,
                        RoleName = role
                    });
                }
                return roleVMObjects;
            }
            return new List<RoleVM>(); // User not found
        }
    }
}
