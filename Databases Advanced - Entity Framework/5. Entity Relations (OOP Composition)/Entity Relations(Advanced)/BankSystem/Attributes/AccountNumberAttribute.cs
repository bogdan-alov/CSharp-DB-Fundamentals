using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Attributes
{
    class AccountNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.ToString().Length < 5)
            {
                return false;
            }
            return true;
        }
    }
}
