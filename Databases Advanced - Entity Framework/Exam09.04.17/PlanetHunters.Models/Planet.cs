namespace PlanetHunters.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal Mass { get; set; }

        [Required]
        public virtual StarSystem HostStarSystem { get; set; }

        public int? DiscoveryId { get; set; }
        public virtual Discovery Discovery { get; set; }
    }
}
