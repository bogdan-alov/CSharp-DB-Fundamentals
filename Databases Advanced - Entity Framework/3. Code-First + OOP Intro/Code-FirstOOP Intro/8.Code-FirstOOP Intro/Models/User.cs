namespace _8.Code_FirstOOP_Intro
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static System.Net.Mime.MediaTypeNames;

    public class User
    {
        public int Id { get; set; }


        [Required]
        [StringLength(30)]
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (value.Length < 4)
                    throw new ArgumentOutOfRangeException("Username must be 4 chars at least!");

                username = value;
            }
        }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        public byte[] ProfilePicture  { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Age must be positive number!");

                if (value > 120)
                {
                    throw new ArgumentOutOfRangeException("Age can't be over 120!");
                }

                age = value;
            }
        }

        public bool isDeleted { get; set; }
    }
}
