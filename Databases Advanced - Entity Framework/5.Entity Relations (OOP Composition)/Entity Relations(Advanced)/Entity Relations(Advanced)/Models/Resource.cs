using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Relations_Advanced_.Models
{
    public enum ResourceType
    {
        Video,
        Presentation,
        Document,
        Other
    }
    public class Resource
    {
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        private ResourceType resource;
        [Required]
        public ResourceType ResourceType
        {
            get
            {
                return resource;
            }
            set
            {
                resource = value;
            }
        }

        [Required]
        public Course CourseId { get; set; }

        [Required]
        public string URL { get; set; }


        public virtual ICollection<License> Licenses { get; set; }
    }
}
