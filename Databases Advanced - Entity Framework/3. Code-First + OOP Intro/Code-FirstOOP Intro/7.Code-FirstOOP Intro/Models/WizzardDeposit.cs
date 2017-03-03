using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7.Code_FirstOOP_Intro.Models
{
    public class WizzardDeposit
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Only positive values are allowed");

                age = value;
            }
        }

        [StringLength(100)]
        public string MagicWandCreator { get; set; }

        public int MagicWandSize { get; set; }

        [StringLength(20)]
        public string  DepositGroup { get; set; }


        public DateTime DepositStartDate { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DepositInterest { get; set; }

        public decimal DepositCharge { get; set; }

        public DateTime DepositExpirationDate { get; set; }

        public bool isDepositExpired { get; set; }

    }
}
