using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class Planet
    {
        public Planet()
        {
            this.People = new HashSet<Person>();
            this.TargettingAnomalies = new HashSet<Anomaly>();
            this.OriginAnomalies = new HashSet<Anomaly>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [ForeignKey("Sun")]
        public int? SunId { get; set; }

        [ForeignKey("SolarSystem")]
        public int? SolarSystemId { get; set; }
        public virtual Star Sun { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        [InverseProperty("OriginPlanet")]
        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }

        [InverseProperty("TeleportPlanet")]
        public virtual ICollection<Anomaly> TargettingAnomalies { get; set; }

        [InverseProperty("HomePlanet")]
        public virtual ICollection<Person> People { get; set; }
    }
}
