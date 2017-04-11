using PlanetHunters.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlanetHunters.Data.Store
{
    public class DiscoveryStore
    {
        public static void AddDiscoveries(IEnumerable<XElement> discoveries)
        {
            using (var ctx = new PlanetHunterContext())
            {
                foreach (var discoveryXML in discoveries)
                {
                    var date = Convert.ToDateTime(discoveryXML.Attribute("DateMade").Value);
                    var telescopeName = discoveryXML.Attribute("Telescope").Value;
                    var telescope = ctx.Telescopes.FirstOrDefault(c => c.Name == telescopeName);
                    var stars = discoveryXML.Element("Stars")?.Elements();
                    var starNames = new List<string>();
                    if (stars != null)
                    {
                        foreach (var starXML in stars)
                        {
                            if (ctx.Stars.Any(c => c.Name == starXML.Value))
                            {
                                starNames.Add(starXML.Value);
                            }
                        }
                    }

                    var planets = discoveryXML.Element("Planets")?.Elements();
                    var planetNames = new List<string>();
                    if (planets != null)
                    {
                        foreach (var planetXML in planets)
                        {
                            if (ctx.Planets.Any(c => c.Name == planetXML.Value))
                            {
                                planetNames.Add(planetXML.Value);
                            }
                        }
                    }

                    var pioneers = discoveryXML.Element("Pioneers")?.Elements();
                    var pioneersNames = new List<string>();
                    if (pioneers != null)
                    {
                        foreach (var astronomerXML in pioneers)
                        {
                            var fullName = astronomerXML.Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                            var firstName = fullName[1];
                            var lastName = fullName[0];
                            if (ctx.Astronomers.Any(c => c.FirstName == firstName && c.LastName == lastName))
                            {
                                var fullNameString = firstName + " " + lastName;
                                pioneersNames.Add(fullNameString);
                            }
                        }
                    }
                    var observersNames = new List<string>();
                    var observers = discoveryXML.Element("Observers")?.Elements();
                    if (observers != null)
                    {
                        foreach (var astronomerXML in observers)
                        {
                            var fullName = astronomerXML.Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                            var firstName = fullName[1];
                            var lastName = fullName[0];
                            if (ctx.Astronomers.Any(c => c.FirstName == firstName && c.LastName == lastName))
                            {
                                var fullNameString = firstName + " " + lastName;
                                observersNames.Add(fullNameString);
                            }
                        }
                    }
                    var pioneersObject = new List<Astronomer>();
                    var observersObject = new List<Astronomer>();
                    foreach (var observerName in observersNames)
                    {
                        var array = observerName.Split(' ').ToArray();
                        var firstName = array[0];
                        var lastName = array[1];
                        if (ctx.Astronomers.Any(c => c.FirstName == firstName && c.LastName == lastName))
                        {
                            var observerAstronomer = ctx.Astronomers.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
                            observersObject.Add(observerAstronomer);
                        }
                    }

                    foreach (var pioneerName in pioneersNames)
                    {
                        var array = pioneerName.Split(' ').ToArray();
                        var firstName = array[0];
                        var lastName = array[1];
                        if (ctx.Astronomers.Any(c => c.FirstName == firstName && c.LastName == lastName))
                        {
                            var pioneerAstronomer = ctx.Astronomers.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
                            pioneersObject.Add(pioneerAstronomer);
                        }
                    }
                    var planetsObject = new List<Planet>();
                    foreach (var planetName in planetNames)
                    {
                        if (ctx.Planets.Any(c => c.Name == planetName))
                        {
                            var planet = ctx.Planets.FirstOrDefault(c => c.Name == planetName);
                            planetsObject.Add(planet);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    var starsObject = new List<Star>();
                    foreach (var starName in starNames)
                    {
                        if (ctx.Stars.Any(c => c.Name == starName))
                        {
                            var star = ctx.Stars.FirstOrDefault(c => c.Name == starName);
                            starsObject.Add(star);
                        }
                    }

                    if (date != null)
                    {
                        var discovery = new Discovery()
                        {
                            Date = date,
                            Telescope = telescope,
                            Observers = observersObject,
                            Pioneers = pioneersObject,
                            Planets = planetsObject,
                            Stars = starsObject
                        };
                        ctx.Discoveries.Add(discovery);
                        Console.WriteLine($"Discovery ({discovery.Date.Date}-{discovery.Telescope.Name}) with {starsObject.Count} star(s), {planetsObject.Count} planet(s), {pioneersObject.Count} pioneer(s) and {observersObject.Count} observers succesfully imported! ");

                        ctx.SaveChanges();
                        
                    }


                }

            }
        }
    }
}
