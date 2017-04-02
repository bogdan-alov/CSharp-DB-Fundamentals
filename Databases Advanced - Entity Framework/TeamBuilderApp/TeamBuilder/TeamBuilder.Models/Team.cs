using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBuilder.Models
{
    public class Team
    {

        public Team()
        {
            this.Members = new HashSet<User>();
            this.ParticipatedEvents = new HashSet<Event>();
        }
        public int Id { get; set; }

        [MaxLength(25)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [StringLength(3, MinimumLength = 3)]
        [Required]
        public string Actronym { get; set; }

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public virtual ICollection<Event> ParticipatedEvents { get; set; }
    }
}
