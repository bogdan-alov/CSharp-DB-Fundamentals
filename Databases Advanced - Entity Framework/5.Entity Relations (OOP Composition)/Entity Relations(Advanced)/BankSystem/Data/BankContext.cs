namespace BankSystem.Data
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BankContext : DbContext
    {
        public BankContext()
            : base("name=BankContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BankContext, Configuration>());
        }


        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<SavingAccount> SavingAccounts { get; set; }

        public virtual DbSet<CheckingAccount> CheckingAccounts { get; set; }
    }
}