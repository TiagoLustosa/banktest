using BankAccountTransactions.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Domain.Repository
{
    public interface IAccountRepository: IRepository<Account, Guid>
    {
    }
}
