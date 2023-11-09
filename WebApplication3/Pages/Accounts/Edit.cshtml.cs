using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.Accounts
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly WebApplication3.Data.ApplicationDbContext _context;
        ClientAccountRepo _clientAccountRepo = null;
        public EditModel(WebApplication3.Data.ApplicationDbContext context)
        {
            _context = context;
            _clientAccountRepo = new ClientAccountRepo(context);
        }

        [BindProperty]
        public ClientAccountVM ClientAccountVM { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int ClientID, int AccountNum)
        {
            if (_context.ClientAccountVM == null)
            {
                return NotFound();
            }

            var clientaccountvm = await _clientAccountRepo.GetAccountDetails(ClientID, AccountNum);
            if (clientaccountvm == null)
            {
                return NotFound();
            }
            ClientAccountVM = clientaccountvm;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var editAccount = await _clientAccountRepo.EditAccount(ClientAccountVM);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientAccountVMExists(ClientAccountVM.AccountNum))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new
            {
                ClientID = ClientAccountVM.ClientID,
                AccountNum = ClientAccountVM.AccountNum
            });
        }

        private bool ClientAccountVMExists(int id)
        {
          return (_context.ClientAccountVM?.Any(e => e.AccountNum == id)).GetValueOrDefault();
        }
    }
}
