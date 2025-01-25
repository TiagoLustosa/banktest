using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Domain.Dto
{
    public class TransactionDto
    {

        public string SenderDocument { get; set; }
        public string ReceiverDocument { get; set; }
        public decimal Amount { get; set; }
        public TransactionDto(string senderDocument, string receiverDocument, decimal amount)
        {
            SenderDocument = senderDocument;
            ReceiverDocument = receiverDocument;
            Amount = amount;
        }
    }
}
