

using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactions.Domain
{
    public class Account : IEntity<Guid>
    {
        public Account(Guid id, Guid customerId, string accountNumber, double balance) : base(id)
        {
            CustomerId = customerId;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public Guid CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        
    }
}
