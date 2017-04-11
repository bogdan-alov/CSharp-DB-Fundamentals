namespace PlanetHunters.ExportData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            // ExportJson.ExportPlanets();

            // ExportJson.ExportAstronomers();

            ExportXml.ExportAllStars();
        }
    }
}
