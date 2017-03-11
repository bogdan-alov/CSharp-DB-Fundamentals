namespace BankSystem.Migrations
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BankSystem.Data.BankContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        //protected override void Seed(BankContext ctx)
        //{
        //    ctx.CheckingAccounts.AddOrUpdate(c => c.AccountNumber,
        //        new CheckingAccount { AccountNumber = "123BAN321", Fee = 100, Balance = 5 },
        //        new CheckingAccount { AccountNumber = "456BAN321", Fee = 10000, Balance = 5000000 },
        //        new CheckingAccount { AccountNumber = "124BAN321", Fee = 1000, Balance = 250 },
        //        new CheckingAccount { AccountNumber = "123NAB321", Fee = 10, Balance = 500 },
        //        new CheckingAccount { AccountNumber = "BN1231-312", Balance = 10000 }
        //        );

        //    ctx.SavingAccounts.AddOrUpdate(c => c.AccountNumber,
        //        new SavingAccount { AccountNumber = "MES1235672",Balance = 5000, InterestRate = 10},
        //        new SavingAccount { AccountNumber = "QUE1235672", Balance = 50000, InterestRate = 1 },
        //        new SavingAccount { AccountNumber = "UN12356722", Balance = 15000, InterestRate = 3 },
        //        new SavingAccount { AccountNumber = "CLUB123567", Balance = 200000, InterestRate = 5 },
        //        new SavingAccount { AccountNumber = "MES1236773", Balance = 30000, InterestRate = 6 },
        //        new SavingAccount { AccountNumber = "MESS113564", Balance = 4000, InterestRate = 7 },
        //        new SavingAccount { AccountNumber = "MES1235675", Balance = 100, InterestRate = 3 },
        //        new SavingAccount { AccountNumber = "MES1235678", Balance = 5, InterestRate = 5 },
        //        new SavingAccount { AccountNumber = "MES1235677", Balance = 8, InterestRate = 4 }


        //        );

        //    ctx.SaveChanges();
        //    base.Seed(ctx);
        //}
    }
}
