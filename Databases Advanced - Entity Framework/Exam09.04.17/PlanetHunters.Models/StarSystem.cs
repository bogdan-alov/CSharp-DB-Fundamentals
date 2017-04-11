namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StarSystem
    {
        public StarSystem()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty("HostStarSystem")]
        public virtual ICollection<Star> Stars { get; set; }

        [InverseProperty("HostStarSystem")]
        public virtual ICollection<Planet> Planets { get; set; }
    }
}
