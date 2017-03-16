using System;
using Gringotts.Data;
using System.Linq;

namespace Gringotts
{
    class Startup
    {
        static void Main()
        {
            using (var ctx = new GringottsContext())
            {
                //19. DepositsSumForOlivanderFamily(ctx);

                DepositsFilter(ctx);
            }
        }

        private static void DepositsFilter(GringottsContext ctx)
        {
            var deposits = ctx.WizzardDeposits.Where(c => c.MagicWandCreator == "Ollivander family").GroupBy(c => c.DepositGroup).Select(c => new { DepositGroup = c.Key, Sum = c.Sum(b => b.DepositAmount) }).OrderByDescending(c => c.Sum).Where(c => c.Sum < 150000).ToList();
            foreach (var deposit in deposits)
            {
                Console.WriteLine($"{deposit.DepositGroup} - ${deposit.Sum}");
            }
        }

        private static void DepositsSumForOlivanderFamily(GringottsContext ctx)
        {
            var depositGroups = ctx.WizzardDeposits.Where(c => c.MagicWandCreator == "Ollivander family").GroupBy(c => c.DepositGroup).Select(c => new { DepositGroup = c.Key, Sum = c.Sum(b => b.DepositAmount) }).ToList();

            foreach (var depositGroup in depositGroups)
            {
                Console.WriteLine($"{depositGroup.DepositGroup} - ${depositGroup.Sum}");
            }
        }
    }
}
