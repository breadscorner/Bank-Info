using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.ViewModel;

namespace WebApplication3.Data
{
    // Banking
    public class ClientAccount
    {
        [Key, Column(Order = 0)]
        public int ClientID { get; set; }

        [Key, Column(Order = 1)]
        public int AccountNum { get; set; }
        public virtual Client Client { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }

    public class Client
    {
        [Key]
        [Required]
        public int ClientID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
    }

    public class BankAccount
    {
        [Key]
        [Required]
        public int AccountNum { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication3.ViewModel.RoleVM> RoleVM { get; set; } = default!;
        public DbSet<WebApplication3.ViewModel.UserVM> UserVM { get; set; } = default!;
        public DbSet<WebApplication3.ViewModel.UserRoleVM> UserRoleVM { get; set; } = default!;
        // assignment one new stuff
        public DbSet<ClientAccount> ClientAccounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientAccount>()
                .HasKey(ca => new { ca.ClientID, ca.AccountNum });
                
            modelBuilder.Entity<ClientAccount>()
                .HasOne(ca => ca.Client)
                .WithMany(c => c.ClientAccounts)
                .HasForeignKey(fk => new { fk.ClientID })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAccount>()
                .HasOne(ca => ca.BankAccount)
                .WithMany(ba => ba.ClientAccounts)
                .HasForeignKey(fk => new { fk.AccountNum })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankAccount>().HasData(
        new BankAccount
        {
            AccountNum = 1,
            AccountType = "Savings",
            Balance = 1000.0m
        },
        new BankAccount
        {
            AccountNum = 2,
            AccountType = "Chequing",
            Balance = 2500.0m
        });

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientID = 1,
                    LastName = "Doe",
                    FirstName = "John",
                    Email = "john.doe@example.com"
                },
                new Client
                {
                    ClientID = 2,
                    LastName = "Smith",
                    FirstName = "Jane",
                    Email = "jane.smith@example.com"
                });

            modelBuilder.Entity<ClientAccount>().HasData(
                new ClientAccount
                {
                    ClientID = 1,
                    AccountNum = 1,
                },
                new ClientAccount
                {
                    ClientID = 2,
                    AccountNum = 2,
                });
        }

        public DbSet<WebApplication3.ViewModel.ClientAccountVM> ClientAccountVM { get; set; } = default!;
    }
}
