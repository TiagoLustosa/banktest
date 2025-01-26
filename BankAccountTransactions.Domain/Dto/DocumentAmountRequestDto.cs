using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Domain.Dto
{
    public class DocumentAmountRequestDto
    {
        public string Document { get; set; }
        public decimal Amount { get; set; }
    }
}
