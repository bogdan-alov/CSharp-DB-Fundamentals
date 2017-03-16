using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Relations_Advanced_.Models
{
    [Table("Homework")]
    public class Homework
    {

        public enum ContentType
        {
            application,
            pdf,
            zip
        }

        
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        private ContentType contentType;
        [Required]
        public ContentType ContenType
        {
            get
            {
                return contentType;
            }
            set
            {
                contentType = value;
            }
        }

        [Required]
        public DateTime SumbissionDate { get; set; }

        
        public Student StudentId { get; set; }

        
        public Course  CourseId { get; set; }
    }
}
