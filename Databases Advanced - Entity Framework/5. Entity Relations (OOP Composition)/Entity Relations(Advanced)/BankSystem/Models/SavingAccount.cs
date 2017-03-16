namespace BankSystem.Models
{
    using BankSystem.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class SavingAccount
    {
        public SavingAccount()
        {
            this.Balance = 0.00m;
            this.InterestRate = 0.00f;
        }
        public int Id { get; set; }
        
        [AccountNumber]
        [Required]
        public string AccountNumber { get; set; }

     
        public decimal Balance { get; set; }

         public User User { get; set; }
        public float InterestRate { get; set; }

        public decimal DepositMoney(decimal price)
        {
            Console.WriteLine($"{price}$ succesfully added to your balance!");
            return this.Balance = this.Balance + price;
        }

        public decimal WithdrawMoney(decimal price)
        {
            if (this.Balance < price)
            {
                Console.WriteLine("Insufficent funds!");
                return this.Balance;
            }
            Console.WriteLine($"{price}$ succefully withdrawed from your balance!");
            return this.Balance = this.Balance - price;
        }

        public void AddInterest(float interest)
        {
            Console.WriteLine($"{interest} added to your Interest Rate!");
            this.InterestRate += interest;
            this.Balance += (this.Balance * (decimal)this.InterestRate);
            
        }
    }
}
