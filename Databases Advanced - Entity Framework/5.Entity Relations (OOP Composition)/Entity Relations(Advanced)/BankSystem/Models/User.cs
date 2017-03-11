using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class User
    {
        public User()
        {
            this.CheckingAccounts = new HashSet<CheckingAccount>();
            this.SavingAccounts = new HashSet<SavingAccount>();
        }
        public int Id { get; set; }
        //^[^\s\,\@\!\<\>\?\~\"\'\-\(\)\*]*$
        [RegularExpression(@"^[\w]*$", ErrorMessage = "Username must not contain any spaces, commas or special sings!")]
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }


        [Required]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid email!")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,15}$", ErrorMessage = "Invalid password! Password must contain one digit, one upper-case letter, one small-case letter and must be at least 6 symbols long")]
        public string Password { get; set; }

       public virtual ICollection<SavingAccount> SavingAccounts { get; set; }

       public virtual ICollection<CheckingAccount> CheckingAccounts { get; set; }
    }
}
