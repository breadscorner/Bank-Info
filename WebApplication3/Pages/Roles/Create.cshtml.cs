using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication3.Data.ApplicationDbContext _context;

        public CreateModel(WebApplication3.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public RoleVM RoleVM { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // ModelState.IsValid performs server side model validation.
            if (!ModelState.IsValid || RoleVM == null)
            {
                return Page();
            }

            RoleRepo roleRepo = new RoleRepo(_context);
            var success = await roleRepo.CreateRole(RoleVM);
            if (success)
            {
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }
    }


}
