using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.UserRole
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly WebApplication3.Data.ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;

        public CreateModel(WebApplication3.Data.ApplicationDbContext context,
                IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [BindProperty]
        public UserRoleEditVM UserRoleEditVM { get; set; } = default!;
        public List<UserVM> userVMs { get; set; }
        public List<RoleVM> roleVMs { get; set; }
        public string errorMessage = "";

        public async Task<IActionResult> OnGetAsync()
        {
            string errorMessage = "";
            UserRepo userRepo = new UserRepo(this._context);
            userVMs = await userRepo.All();

            RoleRepo roleRepo = new RoleRepo(this._context);
            roleVMs = await roleRepo.GetAll();
            return Page();
        }

 // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            UserRepo userRepo = new UserRepo(this._context);
            userVMs = await userRepo.All();

            RoleRepo roleRepo = new RoleRepo(this._context);
            roleVMs = await roleRepo.GetAll();

            if (!ModelState.IsValid)
            { // Server-side check.
                errorMessage = "Please ensure that an option from each dropdown is selected and try again.";
                return Page();
            }

            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);
            
            // Check if the role assignment is already exists before adding it.
            if (await userRoleRepo.AddUserRole(UserRoleEditVM.Email, UserRoleEditVM.Role))
            {
                return RedirectToPage("./Index");
            }
            else
            {
                errorMessage = "Role assignment already exists.";
                return Page();
            }
        }
    }
}
