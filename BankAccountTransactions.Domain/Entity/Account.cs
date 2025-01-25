

using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactions.Domain.Entity
{
    public class Account : IEntity<Guid>
    {
        public Account(Guid id, string customerDocument, string accountNumber, decimal balance) : base(id)
        {
            CustomerDocument = customerDocument;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public string CustomerDocument { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        
    }
}
