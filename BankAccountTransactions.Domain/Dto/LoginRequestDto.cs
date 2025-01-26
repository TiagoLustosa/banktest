using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Domain.Dto
{
    public class LoginRequestDto
    {
        public string Document { get; set; }
        public string Password { get; set; }
    }
}
