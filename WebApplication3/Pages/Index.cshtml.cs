using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication3.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication3.Data.ApplicationDbContext context)
        {
            _context = context;        
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Redirect back to the Index page after saving changes
            return RedirectToPage("./Index");
        }
    }
}
