using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.ClientAccount
{
    [Authorize(Roles = "Admin")]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ClientAccountRepo _clientAccountRepo;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
            _clientAccountRepo = new ClientAccountRepo(context);
        }

        public ClientAccountVM ClientAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(int clientID, int accountNum)
        {
            ClientAccount = await _clientAccountRepo.GetAccountDetails(clientID, accountNum);

            if (ClientAccount == null)
            {
                return NotFound(); // Return a 404 status code if the account is not found.
            }

            return Page();
        }
    }
}
