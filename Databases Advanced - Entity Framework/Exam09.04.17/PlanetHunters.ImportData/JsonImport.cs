namespace PlanetHunters.ImportData
{
    using Data.DTO;
    using Data.Store;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    public class JsonImport
    {
        public static void ImportAstronomers()
        {
            var json = File.ReadAllText("../../datasets/astronomers.json");

            var astronomers = JsonConvert.DeserializeObject<List<AstronomerDTO>>(json);

            AstronomerStore.AddAstronomers(astronomers);
        }

        public static void ImportTelescopes()
        {
            var json = File.ReadAllText("../../datasets/telescopes.json");

            var telescopes = JsonConvert.DeserializeObject<List<TelescopeDTO>>(json);

            TelescopeStore.AddTelescopes(telescopes);
        }

        public static void ImportPlanets()
        {
            var json = File.ReadAllText("../../datasets/planets.json");

            var planets = JsonConvert.DeserializeObject<List<PlanetDTO>>(json);

            PlanetStore.AddPlanets(planets);
        }
    }
}
