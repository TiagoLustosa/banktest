using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactionsDomain.Entity
{
    public class Transaction : IEntity<Guid>
    {
        public Transaction(Guid id, string senderDocument, string receiverDocument, double amount) : base(id)
        {
            SenderDocument= senderDocument;
            ReceiverDocument = receiverDocument;
            Amount = amount;
        }

        public string SenderDocument{ get; set; }
        public string ReceiverDocument { get; set; }
        public double Amount { get; set; }
               
    }
}
