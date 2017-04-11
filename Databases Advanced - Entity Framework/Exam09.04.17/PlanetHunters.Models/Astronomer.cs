namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Astronomer
    {
        public Astronomer()
        {
            this.ObserverDiscoveries = new HashSet<Discovery>();
            this.PioneerDiscoveries = new HashSet<Discovery>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<Discovery> PioneerDiscoveries { get; set; }
        public virtual ICollection<Discovery> ObserverDiscoveries { get; set; }
    }
}
