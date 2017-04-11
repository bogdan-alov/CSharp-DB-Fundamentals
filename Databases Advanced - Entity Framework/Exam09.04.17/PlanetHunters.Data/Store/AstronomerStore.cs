namespace PlanetHunters.Data.Store
{
    using Models;
    using PlanetHunters.Data.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AstronomerStore
    {
        public static void AddAstronomers(List<AstronomerDTO> astronomers)
        {
            using (var ctx = new PlanetHunterContext())
            {
                foreach (var astronomerDTO in astronomers)
                {
                    if (astronomerDTO.FirstName != null && astronomerDTO.LastName != null)
                    {
                        var astronomer = new Astronomer()
                        {
                            FirstName = astronomerDTO.FirstName,
                            LastName = astronomerDTO.LastName
                        };
                        ctx.Astronomers.Add(astronomer);
                        Console.WriteLine($"Record {astronomer.FirstName} {astronomer.LastName} successfully imported.");
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided!");
                    }
                }
                ctx.SaveChanges();
            }
            
        }

        public static void ExportAstronmers(string starSystemName)
        {
            //var ctx = new PlanetHunterContext();
            //var astronomers = ctx.Astronomers.Where(c => c.ObserverDiscoveries.Any(s => s.Stars.Any(d => d.HostStarSystem.Name == starSystemName) || c.PioneerDiscoveries.Any(p => p.Stars.Any(f => f.HostStarSystem.Name == starSystemName)))
        }
    }
}
