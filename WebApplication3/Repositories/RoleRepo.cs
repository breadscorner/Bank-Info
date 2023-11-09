using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.ViewModel;

namespace WebApplication3.Repositories
{
    public class RoleRepo
    {
        ApplicationDbContext _context;

        public RoleRepo(ApplicationDbContext context)
        {
            this._context = context;
            var rolesCreated = CreateInitialRoles();
        }
        public async Task<List<RoleVM>> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();

            List<RoleVM> roleList = new List<RoleVM>();

            foreach (var role in roles)
            {
                roleList.Add(new RoleVM() { RoleName = role.Name, Id = role.Id });
            }
            return roleList;
        }
        public async Task<RoleVM> GetRole(string roleName)
        {
            var role = await _context.Roles.Where(r => r.Name == roleName)
                                            .FirstOrDefaultAsync();
            if (role != null)
            {
                return new RoleVM() { RoleName = role.Name, Id = role.Id };
            }
            return null;
        }
        public async Task<bool> CreateRole(RoleVM roleVM)
        {
            var role = await GetRole(roleVM.RoleName);

            if (role != null)
            {
                return false;
            }

            IdentityRole identityRole = new IdentityRole
            {
                Name = roleVM.RoleName,
                Id = roleVM.RoleName,
                NormalizedName = roleVM.RoleName.ToUpper()
            };

            try
            {
                _context.Roles.Add(identityRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CreateInitialRoles()
        {
            // Create roles if none exist.
            // This is a simple way to do it but it would be better to use a seeder.
            string[] roleNames = { "Admin", "Manager", "Customer" };
            foreach (var roleName in roleNames)
            {
                RoleVM roleVM = new RoleVM
                {
                    RoleName = roleName
                };
                var created = await CreateRole(roleVM);
                // Role already exists so exit.
                if (!created)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
