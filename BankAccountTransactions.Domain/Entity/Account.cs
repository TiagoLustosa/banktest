

using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactions.Domain.Entity
{
    public class Account : Entity<Guid>
    {
        public Account()
        {
            
        }
        public Account(string document,  decimal balance) 
        {
            Document = document;
            Balance = balance;
            CreatedAt = DateTime.Now;
            AccountNumber = GenerateAccountNumber();
        }
        public Account(Guid id, string document,  decimal balance, DateTime createdAt) : base(id)
        {
            Document = document;
            AccountNumber = GenerateAccountNumber();
            Balance = balance;
            CreatedAt = createdAt;
        }
        public string Document { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            var prefix = random.Next(10000, 99999);
            var suffix = random.Next(100, 999);
            return $"{prefix}-{suffix}";
        }
    }
}
