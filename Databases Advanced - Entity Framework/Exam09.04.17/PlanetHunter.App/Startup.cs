namespace PlanetHunter.App
{
    using PlanetHunters.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class Startup
    {
        static void Main(string[] args)
        {
            InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            try
            {
                var ctx = new PlanetHunterContext();
                ctx.Database.Initialize(true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
