using PlanetHunters.Data.DTO;
namespace PlanetHunters.Data.Store
{
    using Models;
    using System;
    using System.Collections.Generic;
    public class TelescopeStore
    {
        public static void AddTelescopes(List<TelescopeDTO> telescopes)
        {
            using (var ctx = new PlanetHunterContext())
            {
                foreach (var telescopeDTO in telescopes)
                {
                    if (telescopeDTO.Name != null && telescopeDTO.Name != null)
                    {
                        if (telescopeDTO.MirrorDiameter < 0 )
                        {
                            telescopeDTO.MirrorDiameter = null;
                        }
                        if (telescopeDTO.MirrorDiameter == 0)
                        {
                            telescopeDTO.MirrorDiameter = null;
                        }

                        var telescope = new Telescope()
                        {
                            Location = telescopeDTO.Location,
                            MirrorDiameter = telescopeDTO.MirrorDiameter,
                            Name = telescopeDTO.Name
                        };
                        ctx.Telescopes.Add(telescope);
                        
                        Console.WriteLine($"Record {telescope.Name} successfully imported.");
                        ctx.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided.");
                    }
                }
               
            }
        }
    }
}
