namespace Exam
{
    using Data;
    using DTOs;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    class Startup
    {
        static void Main(string[] args)
        {
            //InitiliazeDatabase();
            var ctx = new MassDefectContext();
            //ImportSolarSystems(ctx);
            //ImportingStars(ctx);
            //ImportingPersons(ctx);
            //ImportingPlanets(ctx);
            //ImportingAnomalies(ctx);
            //ImportingAnomalyVictims(ctx);
            //ImportingNewAnomalies(ctx);


            //ExportPlanetsWhichAreNotOrigin(ctx);

            //ExportPeopleNotInAnomaly(ctx);

            //ExportTopAnomaly(ctx);

            ExportAllAnomalies(ctx);

        }

        private static void ExportAllAnomalies(MassDefectContext ctx)
        {
            var anomalies = ctx.Anomalies.Select(c => new
            {
                Id = c.Id,
                OriginPlanet = c.OriginPlanet.Name,
                TeleportPlanet = c.TeleportPlanet.Name,
                Victims = c.Victims.Select(d =>  new { d.Name } )
            }).OrderBy(c => c.Id).ToList();

            Console.WriteLine();
            var xmlDocument = new XElement("anomalies");
            foreach (var anomaly in anomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", anomaly.Id));
                if (anomaly.OriginPlanet != null)
                {
                    anomalyNode.Add(new XAttribute("origin-planet", anomaly.OriginPlanet));
                }
                if (anomaly.TeleportPlanet != null)
                {
                    anomalyNode.Add(new XAttribute("teleport-planet", anomaly.TeleportPlanet));
                }
                
                var victimsNode = new XElement("victims");
                foreach (var victim in anomaly.Victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim.Name));
                    victimsNode.Add(victimNode);
                }
                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save("../../anomalies.xml");
        }

        private static void ExportTopAnomaly(MassDefectContext ctx)
        {
            var anomaly = ctx.Anomalies.OrderByDescending(c => c.Victims.Count).Select(c => new
            {
                Id = c.Id,
                OriginPlanet = new { Name = c.OriginPlanet.Name },
                TeleportPlanet = new { Name = c.TeleportPlanet.Name },
                VictimsCount = c.Victims.Count
            }).FirstOrDefault();

            var json = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
            File.WriteAllText("../../hey.json", json);
        }

        private static void ExportPeopleNotInAnomaly(MassDefectContext ctx)
        {
            var people = ctx.People.Where(c => c.Anomalies.Count == 0).Select(c => new
            {
                Name = c.Name,
                HomePlanet = new { Name = c.HomePlanet.Name }
            }).ToList();
            Console.WriteLine();
            var json = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText("../../people-with-no-anomalies.json", json);
            Console.WriteLine();
        }

        private static void ExportPlanetsWhichAreNotOrigin(MassDefectContext ctx)
        {
            var planets = ctx.Planets.Where(c => !c.TargettingAnomalies.Any()).Select(c => c.Name);

            var json = JsonConvert.SerializeObject(planets, Formatting.Indented);

            File.WriteAllText("../../planets-which-are-not.json", json);
            Console.WriteLine("ready");
        }

        private static void ImportingNewAnomalies(MassDefectContext ctx)
        {
            var xmlDoc = XDocument.Load("../../datasets/new-anomalies.xml");
            var anomalies = xmlDoc.Root.Elements();
            var anomaliesDTO = new List<AnomalyWithVictimsDTO>();
            foreach (var anomaly in anomalies)
            {
                var originPlanet = anomaly.Attribute("origin-planet")?.Value;
                var teleportPlanet = anomaly.Attribute("teleport-planet")?.Value;
                var newAnomaly = new AnomalyWithVictimsDTO()
                {
                    OriginPlanet = ctx.Planets.FirstOrDefault(c => c.Name == originPlanet),
                    TeleportPlanet = ctx.Planets.FirstOrDefault(c => c.Name == teleportPlanet),
                    Victims = anomaly.Element("victims").Elements().Select(c => c.Attribute("name").Value).ToList()
                };
                anomaliesDTO.Add(newAnomaly);
            }
            Console.WriteLine();
            foreach (var anomalyDTO in anomaliesDTO)
            {
                var anomally = new Anomaly
                {
                    OriginPlanet = anomalyDTO.OriginPlanet,
                    TeleportPlanet = anomalyDTO.TeleportPlanet
                };
                foreach (var victim in anomalyDTO.Victims)
                {
                    var victimEntity = ctx.People.FirstOrDefault(c => c.Name == victim);
                    anomally.Victims.Add(victimEntity);
                }
                ctx.Anomalies.Add(anomally);
                Console.WriteLine("Succesfully imported anomally");

            }
            ctx.SaveChanges();
            Console.WriteLine();
        }

        private static void ImportingAnomalies(MassDefectContext ctx)
        {
            var json = File.ReadAllText("../../datasets/anomalies.json");
            var anomalies = JsonConvert.DeserializeObject<List<AnomalyDTO>>(json);
            foreach (var anomaly in anomalies)
            {
                if (anomaly.OriginPlanet != null && anomaly.TeleportPlanet != null)
                {
                    if (ctx.Planets.Any(c => c.Name == anomaly.OriginPlanet && ctx.Planets.Any(s => s.Name == anomaly.TeleportPlanet)))
                    {
                        var newAnomaly = new Anomaly()
                        {
                            OriginPlanet = ctx.Planets.FirstOrDefault(c => c.Name == anomaly.OriginPlanet),
                            TeleportPlanet = ctx.Planets.FirstOrDefault(c => c.Name == anomaly.TeleportPlanet)
                        };
                        ctx.Anomalies.Add(newAnomaly);
                        ctx.SaveChanges();
                        Console.WriteLine($"Succesfully added new anomaly {anomaly.OriginPlanet} {anomaly.TeleportPlanet}");
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided");
                    }
                }
                else
                {
                    Console.WriteLine("Error. Invalid data provided");
                }
            }
        }

        private static void ImportingPlanets(MassDefectContext ctx)
        {
            var json = File.ReadAllText("../../datasets/planets.json");
            var planets = JsonConvert.DeserializeObject<List<PlanetDTO>>(json);
            foreach (var planet in planets)
            {
                if (planet.Name != null && planet.SolarSystem != null && planet.Sun != null)
                {
                    if (ctx.SolarSystems.Any(c => c.Name == planet.SolarSystem) && ctx.Stars.Any(c => c.Name == planet.Sun))
                    {
                        var newPlanet = new Planet()
                        {
                            Name = planet.Name,
                            SolarSystem = ctx.SolarSystems.FirstOrDefault(c => c.Name == planet.SolarSystem),
                            Sun = ctx.Stars.FirstOrDefault(c => c.Name == planet.Sun)
                        };
                        ctx.Planets.Add(newPlanet);
                        ctx.SaveChanges();
                        Console.WriteLine($"Succesfully added planet {planet.Name} in {planet.SolarSystem} with sun {planet.Sun}");
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided.");
                    }

                }
                else
                {
                    Console.WriteLine("Error. Invalid data provided.");
                }
            }
        }

        private static void ImportingPersons(MassDefectContext ctx)
        {
            var json = File.ReadAllText("../../datasets/persons.json");
            var people = JsonConvert.DeserializeObject<List<PersonDTO>>(json);

            foreach (var person in people)
            {
                if (person.Name != null && person.HomePlanet != null)
                {
                    if (ctx.Planets.Any(c => c.Name == person.HomePlanet))
                    {
                        var newPerson = new Person()
                        {
                            Name = person.Name,
                            HomePlanet = ctx.Planets.FirstOrDefault(c => c.Name == person.HomePlanet)
                        };
                        ctx.People.Add(newPerson);
                        ctx.SaveChanges();
                        Console.WriteLine($"Successfully added human {person.Name} from {person.HomePlanet}");
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data provided.");
                    }
                }
            }

        }

        private static void ImportingAnomalyVictims(MassDefectContext ctx)
        {
            var json = File.ReadAllText("../../datasets/anomaly-victims.json");

            var anomaliesVictims = JsonConvert.DeserializeObject<List<AnomalyVictimsDTO>>(json);
            foreach (var anomalyVictim in anomaliesVictims)
            {
                if (anomalyVictim.Id > 0 && anomalyVictim.Person != null)
                {
                    if (ctx.Anomalies.Any(c => c.Id == anomalyVictim.Id) && ctx.People.Any(c => c.Name == anomalyVictim.Person))
                    {
                        var person = ctx.People.FirstOrDefault(c => c.Name == anomalyVictim.Person);
                        var anomaly = ctx.Anomalies.FirstOrDefault(c => c.Id == anomalyVictim.Id);
                        anomaly.Victims.Add(person);

                        Console.WriteLine($"Added victim {anomalyVictim.Person}.");
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid data");
                    }
                }
                else
                {
                    Console.WriteLine("Error. Invalid data");
                }
            }
            ctx.SaveChanges();
        }

        private static void ImportingStars(MassDefectContext ctx)
        {
            var json = File.ReadAllText("../../datasets/stars.json");
            var stars = JsonConvert.DeserializeObject<List<StarDTO>>(json);
            foreach (var star in stars)
            {
                if (star.Name != null && star.SolarSystem != null && ctx.SolarSystems.Any(c => c.Name == star.SolarSystem))
                {
                    var newStar = new Star()
                    {
                        Name = star.Name,
                        SolarSystem = ctx.SolarSystems.FirstOrDefault(c => c.Name == star.SolarSystem)
                    };
                    ctx.Stars.Add(newStar);
                    ctx.SaveChanges();
                    Console.WriteLine($"Succesfully added {star.Name} in {star.SolarSystem}");
                }
                else
                {
                    Console.WriteLine("Invalid data provided.");
                }
            }
        }

        private static void ImportSolarSystems(MassDefectContext ctx)
        {
            var file = File.ReadAllText("../../datasets/solar-systems.json");
            var solarSystems = JsonConvert.DeserializeObject<List<SolarSystemDTO>>(file);

            foreach (var solarSystem in solarSystems)
            {
                var newSolarSystem = new SolarSystem()
                {
                    Name = solarSystem.Name
                };
                ctx.SolarSystems.Add(newSolarSystem);
                ctx.SaveChanges();
                Console.WriteLine(newSolarSystem.Name);

            }
        }

        private static void InitiliazeDatabase()
        {
            try
            {
                var ctx = new MassDefectContext();
                ctx.Database.Initialize(true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
