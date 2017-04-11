namespace PlanetHunters.ExportData
{
    using Data.Store;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExportJson
    {


        public static void ExportPlanets()
        {
            var telescopeName = Console.ReadLine();
            PlanetStore.ExportPlanets(telescopeName);
        }

        internal static void ExportAstronomers()
        {
            var starSystemName = Console.ReadLine();
            AstronomerStore.ExportAstronmers(starSystemName);
        }
    }
}
