namespace PlanetHunters.Data.Store
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    public class StarStore
    {
        public static void AddStars(IEnumerable<XElement> stars)
        {
            using (var ctx = new PlanetHunterContext())
            {
                foreach (var starXML in stars)
                {
                    var starName = starXML.Element("Name").Value;
                    int temperature = int.Parse(starXML.Element("Temperature").Value);
                    var starSystem = starXML.Element("StarSystem").Value;

                    if (starName != null && temperature >= 2400 && starSystem != null)
                    {
                        if (!ctx.StarSystems.Any(c=> c.Name == starSystem))
                        {
                            var starSystemObject = new StarSystem()
                            {
                                Name = starSystem
                            };

                            ctx.StarSystems.Add(starSystemObject);

                            var star = new Star()
                            {
                                Name = starName,
                                Temperature = temperature,
                                HostStarSystem = starSystemObject
                            };
                            ctx.Stars.Add(star);
                            Console.WriteLine($"Record {star.Name} successfully added.");
                        }
                        else
                        {
                            var starSystemObject = ctx.StarSystems.FirstOrDefault(c => c.Name == starSystem);
                            var star = new Star()
                            {
                                Name = starName,
                                Temperature = temperature,
                                HostStarSystem = starSystemObject
                            };
                            ctx.Stars.Add(star);
                            Console.WriteLine($"Record {star.Name} successfully added.");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided!");
                    }
                }
                ctx.SaveChanges();
            }
            
        }
    }
}
