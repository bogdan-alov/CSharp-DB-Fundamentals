namespace PlanetHunters.ImportData
{
    using Data.Store;
    using System.Xml.Linq;
    public class XMLImport
    {
        public static void ImportStars()
        {
            var xmlDoc = XDocument.Load("../../datasets/stars.xml");
            var stars = xmlDoc.Root.Elements();
            StarStore.AddStars(stars);
        }

        public static void ImportDiscoveries()
        {
            var xmlDoc = XDocument.Load("../../datasets/discoveries.xml");
            var discoveries = xmlDoc.Root.Elements();
            DiscoveryStore.AddDiscoveries(discoveries);
        }
    }
}
