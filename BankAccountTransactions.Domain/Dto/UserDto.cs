using BankAccountTransactions.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Domain.Dto
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public Guid? AccountId { get; set; }
        public UserType UserType { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
    }
}
