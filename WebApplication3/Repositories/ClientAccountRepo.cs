using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.ViewModel;

namespace WebApplication3.Repositories
{
    public class ClientAccountRepo
    {
        private readonly ApplicationDbContext _context;

        public ClientAccountRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ClientAccountVM>> GetAllAccounts()
        {
            var query = from ca in _context.ClientAccounts
                        join c in _context.Clients on ca.ClientID equals c.ClientID
                        join ba in _context.BankAccounts on ca.AccountNum equals ba.AccountNum
                        orderby c.LastName
                        select new ClientAccountVM
                        {
                            AccountNum = ca.AccountNum,
                            ClientID = c.ClientID,
                            AccountType = ba.AccountType,
                            LastName = c.LastName,
                            FirstName = c.FirstName,
                            Balance = ba.Balance
                        };

            return await query.ToListAsync();
        }

        public async Task<ClientAccountVM> GetAccountDetails(int clientID, int accountNum)
        {
            var query = from ca in _context.ClientAccounts
                        join c in _context.Clients on ca.ClientID equals c.ClientID
                        join ba in _context.BankAccounts on ca.AccountNum equals ba.AccountNum
                        where ca.ClientID == clientID && ca.AccountNum == accountNum
                        select new ClientAccountVM
                        {
                            AccountNum = ca.AccountNum,
                            ClientID = c.ClientID,
                            AccountType = ba.AccountType,
                            Balance = ba.Balance,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAccount(ClientAccountVM model)
        {
            try
            {
                // Create and add new client account to the database
                var clientAccount = new ClientAccount
                {
                    ClientID = model.ClientID,
                    AccountNum = model.AccountNum
                };
                _context.ClientAccounts.Add(clientAccount);

                // Create and add a new bank account to the database
                var bankAccount = new BankAccount
                {
                    AccountNum = model.AccountNum,
                    AccountType = model.AccountType,
                    Balance = model.Balance
                };
                _context.BankAccounts.Add(bankAccount);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false; // Something went wrong.
            }
        }

        public async Task<bool> EditAccount(ClientAccountVM model)
        {
            try
            {
                Client client = await _context.Clients.FirstOrDefaultAsync(ca => ca.ClientID == model.ClientID);
                BankAccount bankAccount= await _context.BankAccounts.FirstOrDefaultAsync(ba => ba.AccountNum == model.AccountNum);

                if (client != null && bankAccount != null)
                {

                    // Update bank account properties
                    client.LastName = model.LastName;
                    client.FirstName = model.FirstName;
                    bankAccount.Balance = model.Balance;

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false; // Account not found.
            }
            catch (Exception ex)
            {
                return false; // Something went wrong.
            }
        }

        public async Task<bool> DeleteAccount(int clientID, int accountNum)
        {
            try
            {
                var clientAccount = await _context.ClientAccounts.FirstOrDefaultAsync(ca => ca.ClientID == clientID && ca.AccountNum == accountNum);
                var bankAccount = await _context.BankAccounts.FirstOrDefaultAsync(ba => ba.AccountNum == accountNum);

                if (clientAccount != null && bankAccount != null)
                {
                    _context.ClientAccounts.Remove(clientAccount);
                    _context.BankAccounts.Remove(bankAccount);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false; // Account not found.
            }
            catch (Exception ex)
            {
                return false; // Something went wrong.
            }
        }
        public async Task<IList<ClientAccountVM>> GetAccountsByType(string accountType)
        {
            var query = from ca in _context.ClientAccounts
                        join c in _context.Clients on ca.ClientID equals c.ClientID
                        join ba in _context.BankAccounts on ca.AccountNum equals ba.AccountNum
                        where ba.AccountType == accountType
                        orderby c.LastName
                        select new ClientAccountVM
                        {
                            AccountNum = ca.AccountNum,
                            ClientID = c.ClientID,
                            AccountType = ba.AccountType,
                            LastName = c.LastName,
                            FirstName = c.FirstName,
                            Balance = ba.Balance
                        };

            return await query.ToListAsync();
        }

    }
}
