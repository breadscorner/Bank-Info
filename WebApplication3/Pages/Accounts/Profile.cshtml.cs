using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages.Account
{
    [Authorize]
    public class ProfileModel: PageModel
    {
        private readonly ApplicationDbContext db;
        public ProfileModel(ApplicationDbContext db) => this.db = db;
        public string username = "";
        public Client client = null;
        public BankAccount bankAccount = null;

        public void OnGet()
        {
            username = User.Identity.Name;

            client = db.Clients.Where(em => em.Email == username).FirstOrDefault();
            var userBank = db.ClientAccounts.Where(ci => ci.ClientID == client.ClientID).FirstOrDefault();
            bankAccount = db.BankAccounts.Where(ba => ba.AccountNum == userBank.AccountNum).FirstOrDefault();
        }
    }

}