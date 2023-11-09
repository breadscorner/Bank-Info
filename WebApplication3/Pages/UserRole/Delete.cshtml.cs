using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.UserRole
{
    [Authorize(Roles = "Admin,Manager")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public DeleteModel(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [BindProperty]
        public UserRoleVM UserRoleVM { get; set; }

        public List<UserVM> UserVMs { get; set; }
        public List<RoleVM> RoleVMs { get; set; }

        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            UserRepo userRepo = new UserRepo(_context);
            UserVMs = await userRepo.All();

            RoleRepo roleRepo = new RoleRepo(_context);
            RoleVMs = await roleRepo.GetAll();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserRepo userRepo = new UserRepo(_context);
            UserVMs = await userRepo.All();

            RoleRepo roleRepo = new RoleRepo(_context);
            RoleVMs = await roleRepo.GetAll();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Select an item from both dropdown menus";
                return Page();
            }

            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);

            if (await userRoleRepo.RemoveUserRole(UserRoleVM.Email, UserRoleVM.Role))
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ErrorMessage = "Role couldn't be deleted or does not exist";
                return Page();
            }
        }
    }
}
