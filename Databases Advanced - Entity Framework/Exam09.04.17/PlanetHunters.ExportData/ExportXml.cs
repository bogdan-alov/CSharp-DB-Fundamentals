using PlanetHunters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.ExportData
{
    public class ExportXml
    {
        internal static void ExportAllStars()
        {
            var ctx = new PlanetHunterContext();
            var stars = ctx.Stars.Select(c => new { Name = c.Name, c.Temperature, StarSystemName = c.HostStarSystem.Name, DiscoveryDate = c.Discovery.Date, TelescopeName = c.Discovery.Telescope.Name, Astronomers = c.Discovery.Pioneers  });


        }
    }
}
