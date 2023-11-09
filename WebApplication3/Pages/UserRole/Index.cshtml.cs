using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.UserRole
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication3.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication3.Data.ApplicationDbContext context)
        { _context = context; }

        public List<UserVM> UserVM { get; set; } = default!;

        public async Task OnGetAsync()
        {
            UserRepo userRepo = new UserRepo(_context);
            UserVM = await userRepo.All();
        }
    }

}
