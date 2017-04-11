namespace PlanetHunters.ImportData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            //JsonImport.ImportAstronomers();
            //JsonImport.ImportTelescopes();
            //JsonImport.ImportPlanets();
            //XMLImport.ImportStars();
            XMLImport.ImportDiscoveries();
        }
    }
}
