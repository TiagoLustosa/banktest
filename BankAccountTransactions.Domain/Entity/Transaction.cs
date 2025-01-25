using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactions.Domain.Entity
{
    public class Transaction : IEntity<Guid>
    {
        public Transaction(Guid id, string senderDocument, string receiverDocument, decimal amount, DateTime transactionDate) : base(id)
        {
            SenderDocument = senderDocument;
            ReceiverDocument = receiverDocument;
            Amount = amount;
            TransactionDate = transactionDate;
        }

        public string SenderDocument{ get; set; }
        public string ReceiverDocument { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
               
    }
}
