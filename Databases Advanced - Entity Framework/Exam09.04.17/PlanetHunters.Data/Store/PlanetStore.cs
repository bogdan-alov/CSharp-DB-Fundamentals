using AutoMapper;
using Newtonsoft.Json;
using PlanetHunters.Data.DTO;
using PlanetHunters.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.Data.Store
{
    public class PlanetStore
    {
        public static void AddPlanets(List<PlanetDTO> planets)
        {
            using (var ctx = new PlanetHunterContext())
            {
                foreach (var planetDTO in planets)
                {
                    if (planetDTO.Name != null && planetDTO.StarSystem != null  )
                    {
                        if (!ctx.StarSystems.Any(c=> c.Name == planetDTO.StarSystem))
                        {
                            var starSystem = new StarSystem()
                            {
                                Name = planetDTO.StarSystem
                            };
                            ctx.StarSystems.Add(starSystem);
                            if (planetDTO.Mass > 0 &&  planetDTO.Mass != 0)
                            {
                                var planet = new Planet()
                                {
                                    Name = planetDTO.Name,
                                    Mass = planetDTO.Mass,
                                    HostStarSystem = starSystem
                                };
                                ctx.Planets.Add(planet);
                                Console.WriteLine($"Record {planet.Name} successfully imported!");
                                ctx.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Error. Invalid data provided!");
                            }


                        }
                        else
                        {

                            var starSystem = ctx.StarSystems.FirstOrDefault(c => c.Name == planetDTO.StarSystem);

                            if (planetDTO.Mass > 0 && planetDTO.Mass != 0)
                            {
                                var planet = new Planet()
                                {
                                    Name = planetDTO.Name,
                                    Mass = planetDTO.Mass,
                                    HostStarSystem = starSystem
                                };
                                ctx.Planets.Add(planet);
                                Console.WriteLine($"Record {planet.Name} successfully imported!");
                                ctx.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Error. Invalid data provided!");
                            }
                        }
                    

                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided!");
                    }
                }
               

            }
        }

        public static void ExportPlanets(string telescopeName)
        {
            var ctx = new PlanetHunterContext();
            if (ctx.Planets.Any(c=> c.Discovery.Telescope.Name == telescopeName))
            {
                var planets = ctx.Planets.Where(c => c.Discovery.Telescope.Name == telescopeName).Select(c => new
                {
                    Name = c.Name,
                    Mass = c.Mass,
                    Orbiting = new { c.HostStarSystem.Name }
                }).OrderByDescending(c=> c.Mass).ToList();

                

                var json = JsonConvert.SerializeObject(planets, Formatting.Indented);
                File.WriteAllText($"../../exports/planets-by-{telescopeName}", json);
            }
            
        }
    }
}
