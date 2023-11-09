using System;
using System.Collections.Generic;
using System.Linq;
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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ClientAccountRepo _clientAccountRepo;

        [BindProperty(SupportsGet = true)]
        public string AccountType { get; set; } = "All"; // Default to "All" if not specified


        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            _clientAccountRepo = new ClientAccountRepo(context);
        }

        //public IList<ClientAccountRepo> ClientAccounts { get; set; } = new List<ClientAccountRepo>();
        public IList<ClientAccountVM> ClientAccounts { get; set; } = new List<ClientAccountVM>();


        public async Task OnGetAsync()
        {
            ClientAccounts = await _clientAccountRepo.GetAllAccounts();

            // Filter the accounts based on the selected account type
            if (AccountType != "All")
            {
                ClientAccounts = await _clientAccountRepo.GetAccountsByType(AccountType);
            }
            else
            {
                ClientAccounts = await _clientAccountRepo.GetAllAccounts();
            }
        }
    }
}
