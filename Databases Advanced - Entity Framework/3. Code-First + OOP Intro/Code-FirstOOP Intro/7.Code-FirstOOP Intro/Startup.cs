

namespace _7.Code_FirstOOP_Intro
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Startup
    {
        static void Main()
        {
            var context = new GringottsContext();
            context.Database.Initialize(true);
            WizzardDeposit dubmledore = new WizzardDeposit()
            {
                FirstName = "Albus",
                LastName = "Dumbledore",
                Age = 150,
                MagicWandCreator = "Antioch Peverell",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24m,
                DepositCharge = 0.2m,
                isDepositExpired = false,
            };

            context.WizzardsDeposits.Add(dubmledore);
            context.SaveChanges();
           
        }
    }
}
