using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Domain.Dto
{
    public class NotificationDto
    {

        public string Email { get; set; }
        public string Message { get; set; }
        public NotificationDto(string email, string message)
        {
            Email = email;
            Message = message;
        }
    }
}
